using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearContoller : MonoBehaviour
{
    [SerializeField] private PlayerController bear;
    [SerializeField] private float pressInterval;
    [SerializeField] public bool running;
    private float runningTime;

    public void StartRunning()
    {
        running = true;
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
