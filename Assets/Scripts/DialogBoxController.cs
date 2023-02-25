using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class DialogBoxController : MonoBehaviour
{
    public int index;
    [SerializeField] private bool canInteract;
    public GameObject blackScreen;
    public GameObject DialogBox;
    public GameObject UIDialog;
    public TextMeshProUGUI dialogBoxText;
    public TextMeshProUGUI button1text;
    public TextMeshProUGUI scoreText;
    private bool keyDown;
    [SerializeField] private int maxIndex;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.GetGameState() == GameState.isPaused || 
            GameManager.gm.GetGameState()== GameState.isGameOver ||
            GameManager.gm.GetGameState()==GameState.isCompleted)
        {
            UIDialog.SetActive(true);
            if (GameManager.gm.GetGameState() == GameState.isPaused)
            {
                dialogBoxText.text = "Game Paused";
                button1text.text = "Continue";
                scoreText.gameObject.SetActive(false);
            }
            else if(GameManager.gm.GetGameState()== GameState.isGameOver)
            {
                dialogBoxText.text = "Game Over";
                button1text.text = "Retry";
                scoreText.gameObject.SetActive(false);
            }
            else if (GameManager.gm.GetGameState() == GameState.isCompleted)
            {
                dialogBoxText.text = "Level Completed";
                button1text.text = "Restart";
                scoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            UIDialog.SetActive(false);
        }
        if(UIDialog.activeSelf)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
        if(Input.GetAxis("Vertical")!=0 && canInteract)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }

        if(Input.GetAxis("Submit")==1 && canInteract)
        {
            ButtonFunction();
        }
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    private void ButtonFunction()
    {
        switch (index)
        {
            case 0:
                Button1Functions();
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Button1Functions()
    {
        switch (button1text.text)
        {
            case "Continue":
                GameManager.gm.SetGameState(GameState.isPlaying);
                break;
            case "Retry":
            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}
