using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime;

    private Vector3 targetPosition;
    private Vector3 currentVelocity;

    public void Awake()
    {
        targetPosition = transform.position;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RunBackward();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RunForward();
        }
        UpdateToRealPosition();
    }

    private void UpdateToRealPosition()
    {
       transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    public void RunForward()
    {
        targetPosition += new Vector3(speed, 0, 0);
    }

    private void RunBackward()
    {
        targetPosition -= new Vector3(speed, 0, 0);
    }
}
