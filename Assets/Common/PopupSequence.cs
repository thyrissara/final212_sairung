using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using System.Linq;

public class PopupSequence : MonoBehaviour
{
    [ContextMenu("Add all animators")]
    public void AddAllAnimators()
    {
        tutorials = this.gameObject.GetComponentsInChildren<Animator>().Where( x => x.name != "Traffic").ToArray();
    }

    [SerializeField] Animator[] tutorials;
    [SerializeField] Animator trafficLight;
    [SerializeField] UnityEvent startGameAction;
    [SerializeField] Image raycastReceiver;

    public bool showTrafficAfterLastPage = false;
    private bool gameStarted = false;
    private bool lightShowing = false;

    private int currentPage = 0;
    private bool sequenceStarted = false;

    public void StartSequence()
    {
        raycastReceiver.raycastTarget = true;
        sequenceStarted = true;
        currentPage = -1;
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
                raycastReceiver.raycastTarget = false;
            }
        }
    }

    public void Touch()
    {
        if(!sequenceStarted) return;
        if(lightShowing) return;

        Hide();
        Advance();
        Show();
        if(currentPage >= tutorials.Length && showTrafficAfterLastPage)
        {
            trafficLight.SetTrigger("Show");
            lightShowing = true;
        }
    }

    public void Hide()
    {
        if(currentPage < 0 || currentPage >= tutorials.Length ) return;
        //Debug.Log("HIDE");
        tutorials[currentPage].SetTrigger("Hide");

        bool finalPage =  currentPage >= tutorials.Length -1;
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
        if ( currentPage >= tutorials.Length ) return;
        //Debug.Log("ADVANCE");
        currentPage++;
    }

    public void Show()
    {
        if(currentPage < 0 || currentPage >= tutorials.Length ) return;
       // Debug.Log("SHOW");
        tutorials[currentPage].SetTrigger("Show");
    }
}
