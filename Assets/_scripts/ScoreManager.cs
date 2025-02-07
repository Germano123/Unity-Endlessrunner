using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public float Meters { get; private set; }
    public float Score { get; private set; }
    [SerializeField] float scoreMultiplier = 1;

    // user interface
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        Score = 0;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: game is not paused
        if (Time.time > 1)
        {
            Meters += Time.deltaTime * scoreMultiplier;
            Score += Time.deltaTime * scoreMultiplier;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        int intScore = (int)Score;
        int count = 0;

        while (intScore >= 1000)
        {
            intScore /= 1000;
            count++;
        }

        scoreText.text = count > 0 ? $"{intScore}{new string('k', count)}" : intScore.ToString();
    }
}
