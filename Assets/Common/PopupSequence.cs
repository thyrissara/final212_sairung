using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class PopupSequence : MonoBehaviour
{
    [SerializeField] Animator[] tutorials;
    [SerializeField] Animator trafficLight;
    [SerializeField] UnityEvent startGameAction;
    [SerializeField] Image raycastReceiver;

    public bool showTrafficAfterLastPage = false;
    private bool gameStarted = false;

    private int currentPage = -1;
    private bool sequenceStarted = false;

    public void StartSequence()
    {
        raycastReceiver.raycastTarget = true;
        sequenceStarted = true;
        Touch();
    }

    public void Update()
    {
        if (trafficLight != null)
        {
            if (trafficLight.GetBool("Finished") == true && !gameStarted)
            {
                startGameAction.Invoke();
                gameStarted = true;
            }
        }
    }

    public void Touch()
    {
        if(!sequenceStarted) return;

        Hide();
        Advance();
        Show();
        if(currentPage >= tutorials.Length && showTrafficAfterLastPage)
        {
            trafficLight.SetTrigger("Show");
        }
    }

    public void Hide()
    {
        if(currentPage < 0) return;
        tutorials[currentPage].SetTrigger("Hide");

        bool finalPage =  currentPage == tutorials.Length -1;
        if (finalPage)
        {
            raycastReceiver.raycastTarget = false;
            if(showTrafficAfterLastPage == false)
            {
                startGameAction.Invoke();
            }
        }
    }

    public void Advance() 
    {
        currentPage++;
    }

    public void Show()
    {
        if(currentPage >= tutorials.Length ) return;
        //Debug.Log("SHOW");
        tutorials[currentPage].SetTrigger("Show");
    }
}
