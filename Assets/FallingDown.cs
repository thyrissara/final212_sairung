using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingDown : MonoBehaviour
{
    public float fallDownSpeed;
    public UnityEvent touchEvent;
    public bool noDestroy;
    public bool isRock;

    public void Update()
    {
        Vector3 pos = this.transform.position;
        pos.y -= fallDownSpeed;
        this.transform.position = pos;

        float destroyY = GameObject.Find("DestroyLine").transform.position.y;
        if(this.transform.position.y < destroyY && !noDestroy)
        {
            GameObject.Destroy(this.gameObject);
            GameObject.FindObjectOfType<Game2Logic>().OutOfBound(isRock);
        }
    }

    public void Touched()
    {
        touchEvent.Invoke();
        GameObject.Destroy(this.gameObject);
    }
}
