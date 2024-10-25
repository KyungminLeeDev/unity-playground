using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) {
            return;
        }

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))  
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position; // 방향 계산
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 이동해야할 위치 계산
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리 속도가 이동에 영향을 주지 않도록 속도 제거
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (!GameManager.instance.isLive) {
            return;
        }
        
        if (!isLive) 
        {
            return;
        }
        
        // 타겟이 왼쪽에 있을때 filp 하기
        spriter.flipX = target.position.x < rigid.position.x;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data) 
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !isLive) {
            return;
        }

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());


        if (health > 0) {
            // Live, Hit Action
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false; // 리지드 바디의 물리적 비활성화
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            // 언데드 사망사운드는 게임종료시에는 나지 않도록 조건 추가
            // 조건추가하지않으면 게임종료시 다수의 언데드가 사망는 사운드를 모두 재생할것
            if (GameManager.instance.isLive) {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }
            
        }


    }

    IEnumerator KnockBack()
    {
        yield return wait;  // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
