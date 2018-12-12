using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] Animator[] tutorials;
    [SerializeField] Animator trafficLight;
    [SerializeField] UnityEvent startGameAction;
    [SerializeField] Image raycastReceiver;
    private bool gameStarted = false;

    private int currentPage = -1;

    public void Awake()
    {
        Touch();
    }

    public void Update()
    {
        if(trafficLight.GetBool("Finished") == true && !gameStarted)
        {
            startGameAction.Invoke();
            gameStarted = true;
        }
    }

    public void Touch()
    {
        Hide();
        Advance();
        Show();
        if(currentPage >= tutorials.Length)
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
        }
    }

    public void Advance() 
    {
        currentPage++;
    }

    public void Show()
    {
        if(currentPage >= tutorials.Length ) return;
        Debug.Log("SHOW");
        tutorials[currentPage].SetTrigger("Show");
    }
}
