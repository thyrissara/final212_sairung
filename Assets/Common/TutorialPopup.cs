using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] Animator[] tutorials;
    [SerializeField] Animator trafficLight;
    [SerializeField] UnityEvent startGameAction;
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
    }

    public void Advance() 
    {
        currentPage++;
    }

    public void Show()
    {
        if(currentPage >= tutorials.Length ) return;
        tutorials[currentPage].SetTrigger("Show");
    }
}
