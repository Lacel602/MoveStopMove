using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        gameOverUI = this.transform.Find("GameOverMenu").gameObject;
    }

    public void RestartGame()
    {
        //Get current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        //Reload current active scene
        SceneManager.LoadScene(currentScene.name);

        gameOverUI.SetActive(false);
    }
}
