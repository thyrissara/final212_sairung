using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2Logic : MonoBehaviour
{
    public PopupSequence tutorial;

    public TextMeshProUGUI orangeTargetText;
    public TextMeshProUGUI bananaTargetText;
    [Space]
    public TextMeshProUGUI orangeCumulativeText;
    public TextMeshProUGUI bananaCumulativeText;
    [Space]
    public GameObject orangePrefab;
    public GameObject bananaPrefab;
    public GameObject rockPrefab;
    [Space]
    public RectTransform spawnFrame;
    public RectTransform spawnParent;

    public WinLoseSequence winLoseSequence;

    public int life = 3;
    public GameObject[] lifeObjects;

    public void Reset()
    {
        SceneManager.LoadScene("Game2");
    }

    public void StartGame()
    {
        StartRound();
    }

    private int orangeTargetCount;
    private int bananaTargetCount;

    private int totalCount;
    private int disappearCount = 0;

    private int collectedOrange = 0;
    private int collectedBanana = 0;

    public void Start()
    {
        tutorial?.StartSequence();
        UpdateLife();
        UpdateScore();
        collectedOrange = 0;
        collectedBanana = 0;
    }

    [ContextMenu("StartRound")]
    public void StartRound()
    {
        StartCoroutine(StartRoundRoutine());
    }

    public Animator bubbleAnimator1;
    public Animator bubbleAnimator2;

    IEnumerator StartRoundRoutine()
    {
        orangeTargetCount = Random.Range(1, 4);
        bananaTargetCount = Random.Range(1, 4);
        int rockCount = Random.Range(0, 3);

        totalCount = orangeTargetCount + bananaTargetCount + rockCount;
        disappearCount = 0;

        ChangeSpeechBubble(orangeTargetCount, bananaTargetCount);

        bubbleAnimator1.SetTrigger("Show");
        bubbleAnimator2.SetTrigger("Show");

        yield return new WaitForSeconds(2);

        bubbleAnimator1.SetTrigger("Hide");
        bubbleAnimator2.SetTrigger("Hide");
        SpawnFruits(orangeTargetCount, bananaTargetCount, rockCount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartRound();
        }
    }

    public void UpdateScore()
    {
        orangeCumulativeText.text = collectedOrange.ToString();
        bananaCumulativeText.text = collectedBanana.ToString();
    }

    public void DecreaseLife()
    {
        life -= 1;
        UpdateLife();
    }

    public void UpdateLife()
    {
        Debug.Log("Updating life to " + life);
        int clampedLife = Mathf.Max(0, life);
        for (int i = 0; i < lifeObjects.Length; i++)
        {
            // 0 1 2 
            // 0

            // 0 1 2 
            // 1

            // 0 1 2 
            // 2

            // 0 1 2 
            // 3

            if (i < clampedLife)
            {
                lifeObjects[i].SetActive(true);
            }
            else
            {
                lifeObjects[i].SetActive(false);
            }
        }
    }

    private void DestroyAllObjects()
    {
        while(spawnParent.transform.childCount > 0)
        {
            GameObject.DestroyImmediate(spawnParent.GetChild(0).gameObject);
        }
    }

    private void SpawnFruits(int orangeCount, int bananaCount, int rockCount)
    {
        //Clear previous fruits
        DestroyAllObjects();

        //Spawn new ones
        List<Rect> allFruitRects = new List<Rect>();

        for (int i = 0; i < orangeCount; i++)
        {
            GameObject go = GameObject.Instantiate(orangePrefab, Vector3.zero, Quaternion.identity, spawnParent);
            var fd = go.GetComponent<FallingDown>();
            fd.noDestroy = false;
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.anchoredPosition = RandomSpawnPosition(spawnFrame);

            Rect r = new Rect(rt.anchoredPosition, rt.sizeDelta);
            allFruitRects.Add(r);
        }
        for (int i = 0; i < bananaCount; i++)
        {
            GameObject go = GameObject.Instantiate(bananaPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            var fd = go.GetComponent<FallingDown>();
            fd.noDestroy = false;
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.anchoredPosition = RandomSpawnPosition(spawnFrame);

            Rect r = new Rect(rt.anchoredPosition, rt.sizeDelta);
            allFruitRects.Add(r);
        }


        for (int i = 0; i < rockCount; i++)
        {
            GameObject go = GameObject.Instantiate(rockPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            for(int j = 0 ; j < 100 ; j++)
            {
                Vector2 pos = RandomSpawnPosition(spawnFrame);
                rt.anchoredPosition = pos;
                Rect rockRect = new Rect(rt.anchoredPosition, rt.sizeDelta);

                bool overlapped = false; ;
                foreach (Rect r in allFruitRects)
                {
                    //Debug.Log($"{rockRect} vs {r}");
                    if (rockRect.Overlaps(r))
                    {
                        overlapped = true;
                    }
                }

                if(overlapped != true)
                {
                    break;
                }
            }
        }

        Vector2 RandomSpawnPosition(RectTransform frame)
        {
            float x = Random.Range(0, frame.sizeDelta.x);
            float y = Random.Range(0, frame.sizeDelta.y);
            return new Vector2(x, y);
        }
    }


    private void ChangeSpeechBubble(int orangeCount, int bananaCount)
    {
        orangeTargetText.text = orangeCount.ToString();
        bananaTargetText.text = bananaCount.ToString();
    }

    public void GetBanana()
    {
        collectedBanana++;
        disappearCount++;
        UpdateScore();
        if(CheckWin() == false)
        {
            CheckStartNewRound();
        }
    }

    public void GetOrange()
    {
        collectedOrange++;
        disappearCount++;
        UpdateScore();
        if(CheckWin() == false)
        {
            CheckStartNewRound();
        }
    }

    bool isWinningOrLosing = false;

    private bool CheckWin()
    {
        if(collectedBanana >= 4 && collectedOrange >= 4)
        {
            isWinningOrLosing = true;
            winLoseSequence.StartSequence(SequenceType.Win);
            return true;
        }
        return false;
    }

    public void OutOfBound(bool isRock)
    {
        if(isWinningOrLosing)
        {
            return;
        }

        disappearCount++;
        if (isRock == false)
        {
            life--;
            UpdateLife();
            if (life == 0)
            {
                isWinningOrLosing = true;
                winLoseSequence?.StartSequence(SequenceType.Lose);
            }
        }

        if (life > 0)
        {
            CheckStartNewRound();
        }
    }

    private void CheckStartNewRound()
    {
        if(disappearCount >= totalCount)
        {
            StartRound();
        }
    }

}
