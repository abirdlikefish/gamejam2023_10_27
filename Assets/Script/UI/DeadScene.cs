using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadScene : MonoBehaviour
{
    private Button restart;

    private Button exit;
    public Text LevelNum;


    void Start()
    {
        restart = GameObject.Find("Restart").GetComponent<Button>();
        exit = GameObject.Find("Exit").GetComponent<Button>();
        LevelNum = GameObject.Find("Level").GetComponent<Text>();
        restart.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
        
        LevelNum.text = "Your final level:" + PlayerPrefs.GetInt("Level");
    }


    void StartGame()
    {
        PlayerPrefs.SetInt("Level",1);
        SceneManager.LoadScene("SampleScene");
    }

    void ExitGame()
    {
        PlayerPrefs.SetInt("Level",1);
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