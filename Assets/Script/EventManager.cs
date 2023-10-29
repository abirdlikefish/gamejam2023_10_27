using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{

    public Action<float> Score;

    public Action<int> Level;
    public Action<float , float> HP;
    

    //Vector3 position, float atk, float begSize, float finSize, int color, float growTime
    //public Action<Vector3, float, float, float, int, float> Enemy_1;

    //Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime
    //public Action<Vector3, float, float, float, float, int, float> Enemy_2;

    public Action<int , Vector3, float, float, float, float, int, float> Enemy;

    public void HPChange(float HP , float maxHP)
    {
        this.HP?.Invoke(HP , maxHP);
    }
    public void UpLevel(int index)
    {
        Level?.Invoke(index);
    }

    public void AddScore(float score)
    {
        Score?.Invoke(score);
    }

    // 0 add maxHP 
    // 1 add speed
    // 2 add begForce
    // 3 add maxForce
    // 4 add charge speed
    // 5 add enemy fly time
    // enemy_1 static enemy
    // enemy_2 normal enemy

    public void CreateEnemy(int enemyType , Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    {
        Enemy?.Invoke(enemyType , position, speed, atk, begSize, finSize, color, growTime);
    }
    //public void CreateEnemy_1(Vector3 position, float atk, float begSize, float finSize, int color, float growTime)
    //{
    //    Enemy_1?.Invoke(position, atk, begSize, finSize, color, growTime);
    //}

    //public void CreateEnemy_2(Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    //{
    //    Enemy_2?.Invoke(position, speed, atk, begSize, finSize, color, growTime);
    //}
    
    
    
    
    public static EventManager Instance { get; private set; }   
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Application.targetFrameRate = 60;
    }

}
