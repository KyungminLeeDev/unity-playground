using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TouchDown()
    {
        lastDongle.Drag();
    }

    public void TouchUp()
    {
        lastDongle.Drop();
    }
}
