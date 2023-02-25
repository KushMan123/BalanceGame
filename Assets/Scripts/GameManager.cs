using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState { isPlaying, isPaused, isGameOver, isCompleted};

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public Score score;
    public TextMeshProUGUI lifeText;
    public AudioClip fallSound;
    public TextMeshProUGUI scoreDialogueText;
    private GameObject player;
    private AudioSource managerAudio;
    private int life = 3;
    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        if (gm == null)
        {
            gm = GetComponent<GameManager>();
        }
        player = GameObject.Find("Player");
        managerAudio = GetComponent<AudioSource>();
        score.SetScore(10000).Begin();
        UpdateLifeUI();
        gameState = GameState.isPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            managerAudio.PlayOneShot(fallSound);
            player.transform.position = CheckpointManager.cm.GetCheckpoint();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.isPaused)
            {
                Debug.Log("Unpaused");
                gameState = GameState.isPlaying;
                score.RestartTimer();
            }
            else if (gameState == GameState.isPlaying)
            {
                Debug.Log("Paused");
                gameState = GameState.isPaused;
                score.StopTimer();
            }
            
        }
        if (life <= 0)
        {
            gameState = GameState.isGameOver;
            Debug.Log("Game Over");
        }

        if (gameState == GameState.isCompleted)
        {
            score.StopTimer();
            int finalScore = score.GetScore() + (life*1000);
            scoreDialogueText.text = "Score: " + finalScore.ToString();
        }
    }

    public void AddLife(int value)
    {
        life += value;
        UpdateLifeUI();
    }

    public int GetLife() 
    {
        return life;
    }

    void UpdateLifeUI()
    {
        lifeText.text = "x " + life.ToString();
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }


}
