using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Game42Logic : MonoBehaviour
{


    public Log2[] logs;
    public Fix[] fixes;
    public PopupSequence tutorialPopup;
    public WinLoseSequence win;
    private bool won;

    public void Awake()
    {
        tutorialPopup.StartSequence();
    }

     public void AfterWinSequence()
    {
        Debug.Log("After Win");
        SceneManager.LoadScene("story5");
    }
    public void Update()
    {
        foreach (Log2 log in logs)
        {
            if (log.State == CandyState.Dragging)
            {
                PointerEventData ped = log.CurrentPointerEventData;

                foreach (Fix f in fixes)
                {
                    //Debug.Log($" {cloud.name} {CheckPedInRect(cloud.selfRect, ped)}");
                    if (
                       f.alreadyFixed == false && CheckPedInRect(f.selfRect, ped)
                    )
                    {
                        f.FixIt();
                        log.InactiveState();
                    }
                }
            }
        }

        if (fixes.All( x => x.alreadyFixed)&& won == false){
            win.StartSequence(SequenceType.Win);
            won = true;
        }
    }

    bool CheckPedInRect(RectTransform rect, PointerEventData pointerEventData)
    {
        if (rect == null)
        {
            return false;
        }

        return RectTransformUtility.RectangleContainsScreenPoint(rect, pointerEventData.position, Camera.main);
    }
}
