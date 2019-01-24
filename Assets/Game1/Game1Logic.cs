using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game1Logic : MonoBehaviour
{
    public Transform player;
    public Transform bear;
    public Transform goal;

    [Space]

    public Transform butterfly1;
    public Transform butterfly2;

    bool goalChecked = false;
    bool butterfly1Spawned = false;
    bool butterfly2Spawned = false;

    [Space]

    public Transform cameraTransform;
    public GameObject butterflyPrefab;

    public PopupSequence tutorialPopup;
    public WinLoseSequence endingPopup;

    public void Awake()
    {
        tutorialPopup.StartSequence();
    }

    public void Update()
    {
        CheckForGoal();
        CheckForButterfly();
    }

    private void CheckForGoal()
    {
        if (!goalChecked)
        {
            if (player.position.x > goal.position.x)
            {
                goalChecked = true;
                endingPopup.StartSequence(SequenceType.Win);
            }
            if (bear.position.x > goal.position.x)
            {
                goalChecked = true;
                endingPopup.StartSequence(SequenceType.Lose);
            }
        }
    }

    public void AfterWinSequence()
    {
        Debug.Log("After Win");
        SceneManager.LoadScene("Story");
    }

    public void AfterLoseSequence()
    {
        Debug.Log("After Lose");
        SceneManager.LoadScene("Game1");
    }

    private void CheckForButterfly()
    {
        if(player.position.x > butterfly1.position.x && !butterfly1Spawned)
        {
            butterfly1Spawned = true;
            SpawnButterfly();
        }
        if(player.position.x > butterfly2.position.x && !butterfly2Spawned)
        {
            butterfly2Spawned = true;
            SpawnButterfly();
        }
    }

    [Range(0, 1f)]
    public float butterflyHeightPercentage = 0.65f;
    private void SpawnButterfly()
    {
        var butterflySpawnPosition = cameraTransform.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1, butterflyHeightPercentage, 0));
        butterflySpawnPosition.z = 0;
        GameObject.Instantiate(butterflyPrefab, butterflySpawnPosition, Quaternion.identity);
    }


}
