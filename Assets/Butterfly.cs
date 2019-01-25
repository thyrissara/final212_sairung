using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    public AudioSource audioSource;
    public float movementPerFrame = 0.1f;
    public bool touched = false;

    public void Update()
    {
        this.transform.position = this.transform.position - (new Vector3(movementPerFrame, 0, 0));
    }

    public void PlayEffect()
    {
    }

    public void Touched()
    {
        if (!touched)
        {
            audioSource.Play();
            touched = true;
            PlayEffect();
            PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
            for (int i = 0; i < 20; i++)
            {
                pc.RunForward();
            }
        }
    }
}
