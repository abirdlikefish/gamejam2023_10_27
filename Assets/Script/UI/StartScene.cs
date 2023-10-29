using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private Button start;

    private Button exit;

    void Start()
    {
        start = GameObject.Find("Start").GetComponent<Button>();
        exit = GameObject.Find("Exit").GetComponent<Button>();
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
    }


    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ExitGame()
    {
        Quit();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}