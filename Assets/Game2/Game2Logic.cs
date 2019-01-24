using TMPro;
using UnityEngine;

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
    [Space]
    public RectTransform spawnFrame;
    public RectTransform spawnParent;

    void Awake()
    {
        tutorial.StartSequence();
    }

    public void StartGame()
    {
        StartRound();
    }


    private int orangeCount;
    private int bananaCount;
    private int collectedOrange = 0;
    private int collectedBanana = 0;
    private int cumulativeOrange = 0;
    private int cumulativeBanana = 0;

    [ContextMenu("StartRound")]
    private void StartRound()
    {
        orangeCount = Random.Range(1, 4);
        bananaCount = Random.Range(1, 4);
        collectedOrange = 0;
        collectedBanana = 0;

        SpawnFruits(orangeCount, bananaCount);
        ChangeSpeechBubble(orangeCount, bananaCount);
    }

    private void SpawnFruits(int orangeCount, int bananaCount)
    {
        for (int i = 0; i < orangeCount; i++)
        {
            GameObject go = GameObject.Instantiate(orangePrefab, Vector3.zero, Quaternion.identity, spawnParent);
            var rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = RandomSpawnPosition(spawnFrame);
        }
        for (int i = 0; i < bananaCount; i++)
        {
            GameObject go = GameObject.Instantiate(bananaPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            var rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = RandomSpawnPosition(spawnFrame);
        }

        Vector2 RandomSpawnPosition(RectTransform frame)
        {
            //TODO: Why randomized into the same position?
            float x = Random.Range( frame.sizeDelta.x, frame.sizeDelta.x);
            float y = Random.Range( frame.sizeDelta.y, frame.sizeDelta.y);
            return new Vector2(x, y);
        }
    }


    private void ChangeSpeechBubble(int orangeCount, int bananaCount)
    {
        orangeTargetText.text = orangeCount.ToString();
        bananaTargetText.text = bananaCount.ToString();
    }

    public void OutOfBound()
    {
    }

}
