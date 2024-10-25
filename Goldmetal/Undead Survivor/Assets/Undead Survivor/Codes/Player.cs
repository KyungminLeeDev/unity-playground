using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    // 기존 인풋매니저 방법 주석처리
    // void Update()
    // {   
    //     // GetAxis는 보정이 들어가서 부드럽게 멈춤
    //     // GetAxisRaw는 더욱 명확한 컨트롤
    //     // 게임 스타일에 맞게 선택
    //     inputVec.x = Input.GetAxisRaw("Horizontal");
    //     inputVec.y = Input.GetAxisRaw("Vertical");
    // }

    void FixedUpdate() {
        if (!GameManager.instance.isLive) {
            return;
        }

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value) 
    {
        inputVec = value.Get<Vector2>();
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    void LateUpdate() 
    {
        if (!GameManager.instance.isLive) {
            return;
        }
        
        // magnitude : 벡터의 크기만 반환
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
        }
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        if (!GameManager.instance.isLive) {
            return;
        }

        
        // 피격시 체력 감소처리
        GameManager.instance.Health -= Time.deltaTime * 10;

        // 플레이어 사망 처리
        if (GameManager.instance.Health < 0) {
            for (int index = 2; index < transform.childCount; index++) {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

}
