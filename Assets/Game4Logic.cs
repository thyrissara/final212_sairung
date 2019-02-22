using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game4Logic : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    public PopupSequence tutorialPopup;
    //public WinLoseSequence endingPopup;

    public void Awake()
    {
        tutorialPopup.StartSequence();
    }

    public void GetLog()
    {
        score++;
        scoreText.text = score.ToString();
        if(score >= 5)
        {
            SceneManager.LoadScene("story4_2");
        }
    }
}
