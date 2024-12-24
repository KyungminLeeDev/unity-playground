//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBehaviour : MonoBehaviour
{
    public float XSpeed;
    public float YSpeed;
    public float ZSpeed;
    public float multiplier = 0.75F;
    public float TooFar = 5;

    static int BallCount = 0;
    public int BallNumber;

    // Start is called before the first frame update
    void Start()
    {
 
        BallCount++;
        BallNumber = BallCount;
        ResetBall();
    }

    private void ResetBall()
    {
        XSpeed = Random.value * multiplier;
        YSpeed = Random.value * multiplier;
        ZSpeed = Random.value * multiplier;

        transform.position = new Vector3(3 - Random.value * 6, 3 - Random.value * 6, 3 - Random.value * 6);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * XSpeed, Time.deltaTime * YSpeed, Time.deltaTime * ZSpeed);

        XSpeed += multiplier - Random.value * multiplier * 2;
        YSpeed += multiplier - Random.value * multiplier * 2;
        ZSpeed += multiplier - Random.value * multiplier * 2;

        if ((Mathf.Abs(transform.position.x) > TooFar) 
            || (Mathf.Abs(transform.position.y) > TooFar)
            || (Mathf.Abs(transform.position.z) > TooFar))
        {
            ResetBall();
        }

    }

    void OnMouseDown()
    {
        GameController controller = Camera.main.GetComponent<GameController>();
        if (!controller.GameOver)
        {
            controller.ClickedOnBall();
            Destroy(gameObject);
        }
    }
        
}
