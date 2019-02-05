using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TouchFollower : MonoBehaviour
{
    public Image image;
    public RectTransform canvasRect;
    public ParticleSystem particle;
    public AudioSource audioSource;

    public void Show() => image.gameObject.SetActive(true);
    public void Hide() => image.gameObject.SetActive(false);

    public void Update()
    {
        if(canvasRect == null) return;

        if (stopTimerTime > 0)
        {
            stopTimerTime -= Time.deltaTime;
            if (stopTimerTime < 0)
            {
                StopParticle();
            }
        }

        if (this.GetComponent<RectTransform>().anchoredPosition.x >= (canvasRect.sizeDelta / 2.0f).x)
        {
            FlipToRight();
        }
        else
        {
            FlipToLeft();
        }
    }

    public float timeToStopParticleWithoutMovement = 0.2f;
    public float stopTimerTime = 0;

    public void PlayParticle() 
    {
        if(particle.isPlaying == false)
        {
            particle.Play();
            audioSource.Play();
        }
        stopTimerTime = timeToStopParticleWithoutMovement;
    }

    public void StopParticle() 
    {
        particle.Stop();
        audioSource.Stop();
    }

    private void FlipToRight()
    {
        image.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FlipToLeft()
    {
        image.transform.localScale = new Vector3(1, 1, 1);
    }

}
