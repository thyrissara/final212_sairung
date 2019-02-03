using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedStatus
{
    Normal,
    SpeedUp,
    SpeedDown
}

public class BearContoller : MonoBehaviour
{

    [SerializeField] private PlayerController bear;
    [SerializeField] private float pressInterval;
    [SerializeField] public bool running;
    private float runningTime;

    public SpeedStatus speedStatus = SpeedStatus.Normal;

    public float speedUpSpeed;
    public float speedDownSpeed;
    public float normalSpeed;

    [Space]

    public float currentSpeed;
    public float distance;

    public void StartRunning()
    {
        running = true;
    }

    public void SpeedUp()
    {
        speedStatus = SpeedStatus.SpeedUp;
        pressInterval = speedUpSpeed;
    }

    public void SpeedDown()
    {
        speedStatus = SpeedStatus.SpeedDown;
        pressInterval = speedDownSpeed;
    }

    public void NormalSpeed()
    {
        speedStatus = SpeedStatus.Normal;
        pressInterval = normalSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if(!running) return;

        runningTime += Time.deltaTime;
        if(runningTime > pressInterval)
        {
            bear.RunForward();
            runningTime = 0;
        }
    }
}
