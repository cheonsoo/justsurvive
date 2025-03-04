using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressPlayAgain()
    {
        Debug.Log("### Play Again");
        SceneManager.LoadScene("GameScene");
    }

    public void OnPressMainMenu()
    {
        Debug.Log("### Main Menu");
        SceneManager.LoadScene("MainMenuScene");
    }
}
