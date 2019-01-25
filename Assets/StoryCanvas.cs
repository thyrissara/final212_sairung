using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryCanvas : MonoBehaviour
{
    public StoryText[] storySequence;
    public string goToSceneAfterTheEnd;

    private int currentPage = 0;

    void Start()
    {
        RunStory();
    }

    public void RunStory()
    {
        ShowStoryAtPage(0);
        currentPage = 0;
    }

    public void AdvanceStory()
    {
        currentPage++;
        if(currentPage >= storySequence.Length)
        {
            SceneManager.LoadScene(goToSceneAfterTheEnd);
        }
        else
        {
            ShowStoryAtPage(currentPage);
        }
    }

    private void ShowStoryAtPage(int page)
    {
        StoryText selectedPage = storySequence[page];
        //...
    }
}
