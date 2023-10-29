using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class UI : MonoBehaviour
{
    private bool isPause;

    private GameObject pauseUI;
    public Button continueBt;
    public Button exitBt;

    private int level = 1;
    
    public GameObject LevelUpUI;
    public TextMeshProUGUI LevelUpText;
    public TextMeshProUGUI LevelNum;
    public Slider healthUI;
    public Slider ScoreUI;

    
    public UnityEngine.UI.Button maxHP;
    public UnityEngine.UI.Button speed;
    public UnityEngine.UI.Button begForce;
    public UnityEngine.UI.Button maxForce;
    public UnityEngine.UI.Button ChargeSpeed;
    public UnityEngine.UI.Button flyTime;

    public float score;// 当前分数
    protected float levelUpScore = 2;//下一级所需分数
    protected float levelUpRate = 1.3f;

    protected float healthUp;//血量上限
    protected float currentHealth;//当前血量

    private void Awake()
    {
        EventManager.Instance.HP += HealthChange;
        EventManager.Instance.Score += ScoreChange;
    }

    void Start()
    {
        isPause = false;
        pauseUI = GameObject.Find("Pause");
        LevelUpUI = GameObject.Find("LevelUp");
        healthUI = GameObject.Find("HealthUI").GetComponent<Slider>();
        ScoreUI = GameObject.Find("ScoreUI").GetComponent<Slider>();
        LevelNum = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        
        //添加监听
        continueBt.onClick.AddListener(ContinueBt);
        exitBt.onClick.AddListener(Exit);
        
        maxHP.onClick.AddListener(AddMaxHP);
        speed.onClick.AddListener(AddSpeed);
        begForce.onClick.AddListener(AddBegForce);
        maxForce.onClick.AddListener(AddMaxForce);
        ChargeSpeed.onClick.AddListener(AddAddChargeSpeed);
        flyTime.onClick.AddListener(AddFlyTime);
        
        pauseUI.SetActive(false);
        LevelUpUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        if ( score>=levelUpScore)
        {
            LevelUp();
        }

        HealthUIChange(currentHealth / healthUp);
        ScoreUIChange(score / levelUpScore);
        Dead();
    }
    void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
    void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }
    void LevelUp()
    {
        LevelUpUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseLevelUp()
    {
        LevelUpUI.SetActive(false);
        Time.timeScale = 1f;
        levelUpScore *= levelUpRate;
        score -= levelUpScore;
        level++;
    }

    void AddMaxHP()
    {
        Debug.Log("added maxHP");
        EventManager.Instance.UpLevel(0);
        CloseLevelUp();
    }

    void AddSpeed()
    {
        Debug.Log("added speed");
        EventManager.Instance.UpLevel(1);

        CloseLevelUp();
    }

    void AddBegForce()
    {
        Debug.Log("added begForce");
        EventManager.Instance.UpLevel(2);
        CloseLevelUp();
    }

    void AddMaxForce()
    {
        Debug.Log("added maxforce");
        EventManager.Instance.UpLevel(3);
        CloseLevelUp();
    }

    void AddAddChargeSpeed()
    {
        Debug.Log("added chargeSpeed");
        EventManager.Instance.UpLevel(4);
        CloseLevelUp();
    }

    void AddFlyTime()
    {
        Debug.Log("added flyTime");
        EventManager.Instance.UpLevel(5);
        CloseLevelUp();
    }

    void HealthUIChange(float value)
    {
        healthUI.value = value;
    }
    void ScoreUIChange(float value)
    {
        LevelNum.text = "Level:" + level.ToString();
        ScoreUI.value = value;
    }

    void ContinueBt()
    {
        Pause();
    }

    void Exit()
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
    void HealthChange(float hp, float maxhp)
    {
        currentHealth = hp;
        healthUp = maxhp;
    }

    void ScoreChange(float score)
    {
        this.score += score;
    }

    void Dead()
    {
        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Level",level);
            SceneManager.LoadScene("DeadScene");
        }
    }
}
