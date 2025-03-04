using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressGameStart()
    {
        Debug.Log("### Game Start");
        SceneManager.LoadScene("GameScene");
    }

    public void OnPressExit()
    {
        Debug.Log("### Exit");
        Application.Quit();
    }
}
