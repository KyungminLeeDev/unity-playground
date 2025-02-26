using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라 위치에 플레이어 위치를 빼서 차이 계산
        offset = transform.position - player.transform.position;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
