using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Match3Game;

    [Header("Game Pannels-")]
    public GameObject WinPannel;
    public GameObject LosePannel;
    public GameObject DifficultyPannel;

    [SerializeField] GameObject OpeningText;

    [Header("Audio-")]
    public AudioClip MusicClip;
    public AudioSource MusicSource;

    float GoalScore = 1000;
    bool play = false;
    bool isPlayable = false;
    float timeLeft = 30;

    [Header("Texts-")]
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI timeTxt;

    
    void Awake()
    {
        // Set the pannel for the appropriate opening.
        goalText.SetText("Goal Score = ?");

        Match3Game.SetActive(false);

        WinPannel.SetActive(false);
        LosePannel.SetActive(false);
        DifficultyPannel.SetActive(false);

        OpeningText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetTimer();

        if (Input.GetKeyDown(KeyCode.E) && OpeningText.active)
        {
            isPlayable = true;
            DifficultyPannel.SetActive(true);
            OpeningText.SetActive(false);
        }

        
    }

    public void Easy()
    {
        GoalScore = 250;
        PlayGame();
    }

    public void Medium()
    {
        GoalScore = 350;
        PlayGame();
    }

    public void Hard()
    {
        GoalScore = 450;
        PlayGame();
    }

    void PlayGame()
    {
        // Play the Match 3 Game
        DifficultyPannel.SetActive(false);
        goalText.SetText("Goal Score = " + GoalScore);
        MusicSource.Play();
        Match3Game.SetActive(true);
        play = true;
    }

    void SetTimer()
    {
        // Game Timer
        if (ScoreCounter.Instance.Score >= GoalScore && isPlayable)
        {
            GameOver(WinPannel);
        }

        if (timeLeft > 0 && play)
        {
            timeLeft -= Time.deltaTime * 1;
            timeTxt.text = "Time Left: " + (int)timeLeft;
        }
        else if (timeLeft <= 0 && isPlayable)
        {
            GameOver(LosePannel);
        }
    }

    void GameOver(GameObject gameOverPannel)
    {
        // Game Over whether it's Win or Lose
        isPlayable = false;

        Match3Game.SetActive(false);
        MusicSource.Stop();
        gameOverPannel.SetActive(true);
        play = false;
    }

    public void Quit()
    {
        // Quit Game
        WinPannel.SetActive(false);
        LosePannel.SetActive(false);
    }
}
