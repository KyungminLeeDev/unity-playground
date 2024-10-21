using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    public GameManager manager;
    public int level;
    public bool isDrag;
    public bool isMerge;

    Rigidbody2D rigid;
    CircleCollider2D circle;
    Animator anim;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        anim.SetInteger("Level", level);
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

        StartCoroutine(HideRoutine(tartgetPos));
    }

    IEnumerator HideRoutine(Vector3 tartgetPos)
    {
        int frameCount = 0;

        while(frameCount < 20) {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, tartgetPos, 0.5f);
            yield return null; // 1프레임 쉬기
        }

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

        yield return new WaitForSeconds(0.3f);

        level++;

        // MathF.max() : 인자 값 중에 최대값을 반환하는 함수
        manager.maxLevel = Mathf.Max(level, manager.maxLevel);

        isMerge = false;
    }
    
}
