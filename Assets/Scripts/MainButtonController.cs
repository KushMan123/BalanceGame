using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtonController : MonoBehaviour
{
    public int index;
    public bool canInteract;
    public GameObject mainMenuButtons;
    public GameObject loadingButtons;
    public Image loadingFill;
    public string sceneName;
    [SerializeField] private bool keyDown;
    [SerializeField] int maxIndex;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal")!=0 && canInteract)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Horizontal") < 0)
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
                if (Input.GetAxis("Horizontal") > 0)
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
        if (Input.GetAxis("Submit")==1)
        {
            ButtonFunctionality(index);
        }
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    void ButtonFunctionality(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
                LoadScene();
                break;
            case 1:
                Application.Quit();
                break;
        }
    }

    void LoadScene()
    {
        mainMenuButtons.SetActive(false);
        loadingButtons.SetActive(true);
        StartCoroutine(LoadSceneCorountine());
    }

    IEnumerator LoadSceneCorountine()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);

        while (!scene.isDone)
        {
            float progressValue = Mathf.Clamp01(scene.progress / 0.9f);
            loadingFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
