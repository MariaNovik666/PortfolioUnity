using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
  [SerializeField] private List<BugLevel1> bugs;

  [Header("UI objects")]
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject outOfTimeText;
  [SerializeField] private GameObject bombText;
  [SerializeField] private GameObject gamewin;
  [SerializeField] private TMPro.TextMeshProUGUI timeText;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;

  // Hardcoded variables you may want to tune.
  private float startingTime = 30f;

  // Global variables
  private float timeRemaining;
  private HashSet<BugLevel1> currentMoles = new HashSet<BugLevel1>();
  private int score;
  private bool playing = false;

  // This is public so the play button can see it.
  public void StartGame() {
    // Hide/show the UI elements we don't/do want to see.
    playButton.SetActive(false);
    outOfTimeText.SetActive(false);
    bombText.SetActive(false);
    gameUI.SetActive(true);
    gamewin.SetActive(false);
    // Hide all the visible bugs.
    for (int i = 0; i < bugs.Count; i++) {
      bugs[i].Hide();
      bugs[i].SetIndex(i);
    }
    // Remove any old game state.
    currentMoles.Clear();
    // Start with 30 seconds.
    timeRemaining = startingTime;
    score = 0;
    scoreText.text = "0";
    playing = true;
  }

  public void GameOver(int type) {
    // Show the message.
    if (type == 0) {
      outOfTimeText.SetActive(true);
    } else {
      bombText.SetActive(true);
    }
    // Hide all bugs.
    foreach (BugLevel1 bug in bugs) {
      bug.StopGame();
    }
    // Stop the game and show the start UI.
    playing = false;
    playButton.SetActive(true);
  }

  // Update is called once per frame
  void Update() {
    if (playing) {
      // Update time.
      timeRemaining -= Time.deltaTime;
      if (timeRemaining <= 0) {
        timeRemaining = 0;
        GameOver(0);
      }
      timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
      // Check if we need to start any more bugs.
      if (currentMoles.Count <= (score / 10)) {
        // Choose a random bug.
        int index = Random.Range(0, bugs.Count);
        // Doesn't matter if it's already doing something, we'll just try again next frame.
        if (!currentMoles.Contains(bugs[index])) {
          currentMoles.Add(bugs[index]);
          bugs[index].Activate(score / 10);
        }
      }
    }
  }

  public void AddScore(int moleIndex) {
    // Add and update score.
    score += 1;
    scoreText.text = $"{score}";
    // Increase time by a little bit.
    // Remove from active bugs.
    currentMoles.Remove(bugs[moleIndex]);
    if(score == 30)
        {
            foreach (BugLevel1 bug in bugs)
            {
                bug.StopGame();
            }
            // Stop the game and show the start UI.
            playing = false;
            gamewin.SetActive(true);
        }
  }
}
