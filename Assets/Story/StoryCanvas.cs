using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryCanvas : MonoBehaviour
{
    public StoryText[] storySequence;
    public string goToSceneAfterTheEnd;
    public PlayableDirector inDirector;
    public PlayableDirector outDirector;

    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI whoText;
    public Image bgImage;
    public CanvasGroup whoCanvasGroup;

    public AudioSource changePageSource;

    public GraphicRaycaster graphicRaycaster;

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
        changePageSource.Play();
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
        StartCoroutine(ChangeStoryRoutine(page));
    }

    IEnumerator ChangeStoryRoutine(int page)
    {
        graphicRaycaster.enabled = false;
        //Fade out
        if (page != 0)
        {
            outDirector.Play();
            yield return new WaitForSeconds((float)outDirector.playableAsset.duration);
        }

        //Change
        StoryText selectedPage = storySequence[page];

        narrationText.text = selectedPage.text;
        if (selectedPage.who == "")
        {
            whoCanvasGroup.alpha = 0;
        }
        else
        {
            whoCanvasGroup.alpha = 1;
            whoText.text = selectedPage.who;
        }
        bgImage.sprite = selectedPage.backgroundImage;

        //Fade in
        inDirector.Play();
        yield return new WaitForSeconds((float)inDirector.playableAsset.duration);
        graphicRaycaster.enabled = true;
    }
}
