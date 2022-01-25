using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public int pointsPerSecond;

    public float currentScore;

    // Start is called before the first frame update
    void Start() {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update() {
        currentScore += Time.deltaTime * pointsPerSecond;
    }
}
