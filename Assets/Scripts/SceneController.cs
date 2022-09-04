using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private DarkenScreen darkenScreen;

    private float delayInSeconds = 2f;

    private void Awake()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameEvents.Victory += OnVictory;
    }
   
    private void OnDestroy()
    {
        GameEvents.Victory -= OnVictory;
    }

    private void OnVictory()
    {
        darkenScreen.gameObject.SetActive(true);
        darkenScreen.MakeOpaque();
        StartCoroutine(RestartMainSceneWithDelay());
    }

    private IEnumerator RestartMainSceneWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
