using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private bool shouldStop;

    public int initialScore { get; private set; }

    private int remainingScore;

    private void Awake()
    {
        ResetScore();
    }

    private void ResetScore()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;

        initialScore = remainingScore=0;
    }
    
    public Score SetScore(int score)
    {
        initialScore = remainingScore = score;
        shouldStop = false;
        return this;
    }

    public void Begin()
    {
        if (!shouldStop)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateScore());
        }
        
    }

    private IEnumerator UpdateScore()
    {
        while(remainingScore > 0)
        {
            UpdateUI(remainingScore);
            remainingScore--;
            yield return new WaitForSeconds(1f);
        }
    }

    void  UpdateUI(int score)
    {
        uiText.text = score.ToString();
        uiFillImage.fillAmount = Mathf.InverseLerp(0, initialScore, score);
    }

    public void End()
    {
        ResetScore();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void AddScorePoints(int score)
    {
        remainingScore += score;
    }

    public void StopTimer()
    {
        shouldStop = true;
        StopAllCoroutines();
    }

    public void RestartTimer()
    {
        shouldStop = false;
        StopAllCoroutines();
        StartCoroutine(UpdateScore());
    }

    public int GetScore()
    {
        return remainingScore;
    }
}
