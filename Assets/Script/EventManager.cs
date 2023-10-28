using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{

    public Action<float> GetScore;

    //Vector3 position, float atk, float begSize, float finSize, int color, float growTime
    public Action<Vector3, float, float, float, int, float> Enemy_1;

    //Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime
    public Action<Vector3, float, float, float, float, int, float> Enemy_2;




    // enemy_1 静止敌人
    // enemy_2 运动敌人
    // 通过下面两个函数生成敌人
    // 示例 :
    // EventManager.Instance.CreateEnemy_1(........);
    public void CreateEnemy_1(Vector3 position, float atk, float begSize, float finSize, int color, float growTime)
    {
        Enemy_1?.Invoke(position, atk, begSize, finSize, color, growTime);
    }

    public void CreateEnemy_2(Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    {
        Enemy_2?.Invoke(position, speed, atk, begSize, finSize, color, growTime);
    }
    
    
    
    
    public static EventManager Instance { get; private set; }
    
    
    
    
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

}
