using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text currentScoreText;

    public int pointsPerSecond;

    float _currentScore;
    public float currentScore
    {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            currentScoreText.text = currentScore.ToString("n0");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore += Time.deltaTime * pointsPerSecond;
    }
}
