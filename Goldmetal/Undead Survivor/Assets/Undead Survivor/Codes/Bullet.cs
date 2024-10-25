using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    
    Rigidbody2D rigid;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float damage, int per, Vector3 dir) 
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0) {
            rigid.velocity = dir * 15f;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || per == -100) {
            return;
        }

        per--;

        if (per < 0) {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area") || per == -100) {
            return;
        }

        // 플레이어의 area 밖으로 나가면 바로 비활성화
        gameObject.SetActive(false);
    }
}
