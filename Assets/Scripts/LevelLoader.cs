using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator canvasAnimator;
    public void LoadLevel()
    {
        StartCoroutine(LoadLevel((SceneManager.GetActiveScene().buildIndex + 1) % 2));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        canvasAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(levelIndex);
    }
}
