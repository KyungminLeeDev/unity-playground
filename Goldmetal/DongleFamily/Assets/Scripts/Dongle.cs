using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    public GameManager manager;
    public ParticleSystem effect;
    public int level;
    public bool isDrag;
    public bool isMerge;
    public bool isAttach;

    public Rigidbody2D rigid;
    CircleCollider2D circle;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float deadTime;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        anim.SetInteger("Level", level);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        // 동글 속성 초기화
        level = 0;
        isDrag = false;
        isMerge = false;
        isAttach = false;

        // 동글 트랜스폼 초기화
        transform.localPosition = Vector3.zero;
        transform.localRotation = quaternion.identity;
        transform.localScale = Vector3.zero;

        // 동글 물리 초기화
        rigid.simulated = false;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        circle.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // x축 경계 설정
            float leftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;
            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            // x, y 축 고정
            mousePos.y = 8;
            mousePos.z = 0;

            // 자신의 위치에 변환된 마우스 위치를 적용
            // - Vector3.Lerp : 목표지점으로 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(AttachRoutine());
    }

    IEnumerator AttachRoutine() 
    {
        if (isAttach) {
            yield break; // 코루틴을 탈출하는 키워드
        }

        isAttach = true;
        manager.SfxPlay(GameManager.Sfx.Attach);

        yield return new WaitForSeconds(0.2f);

        isAttach = false;
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dongle") {
            Dongle other = collision.gameObject.GetComponent<Dongle>();

            if (level == other.level && !isMerge && !other.isMerge && level < 7) {
                // 동글 합치기 로직
                
                // 나와 상대편 위치 가져오기
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                // 1. 내가 아래에 있을 때
                // 2. 동일한 높이 일때, 내가 오른쪽에 있을 때
                if (meY < otherY || (meY == otherY && meX > otherX)) {
                    // 상대방은 숨기기
                    other.Hide(transform.position);
                    // 나는 레벨업
                    LevelUp();
                }
            }
        }
    }


    public void Hide(Vector3 tartgetPos)
    {
        isMerge = true;
        
        // 흡수 이동을 위해 물리효과 모두 비활성화
        rigid.simulated = false;
        circle.enabled = false;

        if (tartgetPos == Vector3.up * 100) {
            // 게임오버되어 없앨시에는 파티클 이펙트 실행
            EffectPlay();
        }

        StartCoroutine(HideRoutine(tartgetPos));
    }

    IEnumerator HideRoutine(Vector3 tartgetPos)
    {
        int frameCount = 0;

        while(frameCount < 20) {
            frameCount++;
            if (tartgetPos != Vector3.up * 100) {
                transform.position = Vector3.Lerp(transform.position, tartgetPos, 0.5f);
            }
            else if (tartgetPos == Vector3.up * 100) {
                // 게임오버시
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            }
            
            yield return null; // 1프레임 쉬기
        }

        // Math.Pow() : 지정 숫자의 거듭제곱
        manager.score += (int)Mathf.Pow(2, level);

        // while문이 끝나면 잠금해제하면서 오브젝트 비활성화
        isMerge = false;
        gameObject.SetActive(false);
    }

    void LevelUp()
    {
        isMerge = true;

        // 레벨업중에는 물리속도 제거
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        anim.SetInteger("Level", level + 1);
        EffectPlay();
        manager.SfxPlay(GameManager.Sfx.LevelUp);

        yield return new WaitForSeconds(0.3f);

        level++;

        // MathF.max() : 인자 값 중에 최대값을 반환하는 함수
        manager.maxLevel = Mathf.Max(level, manager.maxLevel);

        isMerge = false;
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Finish") {
            deadTime += Time.deltaTime;

            if (deadTime > 2) {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);
            }
            if (deadTime > 5) {
                manager.GameOver();
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish") {
            deadTime = 0;
            spriteRenderer.color = Color.white;
        }
    }
    

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale;
        effect.Play();
    }
}
