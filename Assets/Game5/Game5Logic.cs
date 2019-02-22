using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Game5State
{
    Feeding,
    Waiting,
    Dropping
}

public class Game5Logic : MonoBehaviour
{

    public WinLoseSequence win;
    private bool won;
   

    public PopupSequence tutorialPopup;
    //public WinLoseSequence endingPopup;

    public void Awake()
    {
        tutorialPopup.StartSequence();
    }

     public void AfterWinSequence()
    {
        Debug.Log("After Win");
        SceneManager.LoadScene("story6");
    }
    
    public Candy[] candies;

    [Space]

    public Cloud[] clouds;
    public Cloud[] blueClouds;
    public Cloud[] purpleClouds;

    [Space]

    public Game5State gameState;

    public PopupSequence tutorial;
    public WinLoseSequence winLoseSequence;

    public void Start()
    {
        tutorial.StartSequence();
    }

    public void NewRound()
    {
        var filteredBlueClouds = blueClouds.Where( x => x.targetCandy != x.candyGot).ToArray();
        var fileredPurpleClouds = purpleClouds.Where( x => x.targetCandy != x.candyGot).ToArray();

        int randomizedBlue = Random.Range(0, filteredBlueClouds.Length);
        int randomizedPurple = Random.Range(0, fileredPurpleClouds.Length);

        filteredBlueClouds[randomizedBlue].SetToAccepting();
        fileredPurpleClouds[randomizedPurple].SetToAccepting();
    }

    public void Update()
    {
        if (gameState == Game5State.Feeding)
        {
            FeedingLogic();
        }
        else if(gameState == Game5State.Dropping)
        {
            DroppingLogic();
        }

        void DroppingLogic()
        {
            if (clouds.All(x => x.CloudState == CloudState.Success) && won == false)
            {
                winLoseSequence.StartSequence(SequenceType.Win);
                won = true;
            }
        }

        void FeedingLogic()
        {
            foreach (Candy candy in candies)
            {
                if (candy.State == CandyState.Dragging)
                {
                    PointerEventData ped = candy.CurrentPointerEventData;

                    foreach (Cloud cloud in clouds)
                    {
                        //Debug.Log($" {cloud.name} {CheckPedInRect(cloud.selfRect, ped)}");
                        if (
                            candy.candyType == cloud.acceptingCandyType &&
                            cloud.CloudState == CloudState.Accepting &&
                            CheckPedInRect(cloud.selfRect, ped)
                        )
                        {
                            cloud.GetCandy();
                            candy.InactiveState();
                        }
                    }
                }
            }

            if(candies.All( x => x.State == CandyState.Inactive))
            {
                gameState = Game5State.Waiting;
                StartCoroutine(WaitingRoutine());
            }

        }
    }

    IEnumerator WaitingRoutine()
    {
        yield return new WaitForSeconds(2.7f);

        bool enough = clouds.All(c => c.candyGot >= c.targetCandy);

        if (enough)
        {
            gameState = Game5State.Dropping;
            foreach(Cloud c in clouds)
            {
                c.EnterDroppingState();
            }
        }
        else
        {
            gameState = Game5State.Feeding;
            foreach (Candy c in candies)
            {
                c.RestoreToActive();
            }
            NewRound();
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
