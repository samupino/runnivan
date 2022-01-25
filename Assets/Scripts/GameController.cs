using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpeedController))]
[RequireComponent(typeof(ScoreController))]
[RequireComponent(typeof(UIController))]
public class GameController : MonoBehaviour {

    public enum State {
        ADVANCING, OVER
    };

    SpeedController speedController;
    ScoreController scoreController;
    UIController uiController;

    State state;

    // Start is called before the first frame update
    void Start() {
        speedController = GetComponent<SpeedController>();
        scoreController = GetComponent<ScoreController>();
        uiController = GetComponent<UIController>();

        state = State.ADVANCING;

        uiController.HideGameOverScreen();
        uiController.UpdateLiveScore(0);
    }

    void Update() {
        uiController.UpdateLiveScore((int)scoreController.currentScore);
    }

    public void Die() {
        switch (state) {
            case State.ADVANCING:
                speedController.Stop();
                uiController.ShowGameOverScreen((int)scoreController.currentScore);
                break;
            default: break;
        }
    }
}
