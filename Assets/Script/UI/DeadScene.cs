using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadScene : MonoBehaviour
{
    private Button restart;

    private Button exit;

    void Start()
    {
        restart = GameObject.Find("Restart").GetComponent<Button>();
        exit = GameObject.Find("Exit").GetComponent<Button>();
        restart.onClick.AddListener(StartGame);
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