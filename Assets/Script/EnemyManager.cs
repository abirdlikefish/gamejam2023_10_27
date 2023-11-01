using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public Sprite[] enemyBody = new Sprite[6];
    public Sprite[] enemyBodyColor = new Sprite[6];
    public Sprite[] enemyEyes = new Sprite[6];
    public Sprite[] enemyMouth = new Sprite[6];
    public Sprite enemyBoss;

    public GameObject player;

    public GameObject prefabEnemy;

    private void Start()
    {
        for(int i = 1 ; i < 6 ; i++)
        {
            enemyBody[i - 1] = Resources.Load("Picture/Enemy/body_" + i.ToString() ,typeof(Sprite)) as Sprite;
            enemyBodyColor[i - 1] = Resources.Load("Picture/Enemy/body_color_" + i.ToString(),typeof(Sprite)) as Sprite;
            enemyEyes[i - 1] = Resources.Load("Picture/Enemy/eye_" + i.ToString() , typeof(Sprite) ) as Sprite ;
        }
    }


    public static EnemyManager Instance{private set ; get;}
    // Start is called before the first frame update
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
