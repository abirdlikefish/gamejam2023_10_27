using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddEnemy : MonoBehaviour
{
    protected Text Text_position; 
    protected Text Text_enemyType; 
    protected Text Text_enemyColor; 

    protected InputField position_x;
    protected InputField position_y;
    protected InputField enemyType;
    protected InputField enemyColor;
    protected Button create;

    public void CreateEnemy()
    {
        EventManager.Instance.CreateEnemy(int.Parse(enemyType.text) , new Vector3(float.Parse( position_x.text),float.Parse( position_y.text), 0) , 1 , 1 , 0.5f , 1 , int.Parse(enemyColor.text) , 1);
        Debug.Log("create Enemy");
    }

    // Start is called before the first frame update
    void Start()
    {
        Text_position = transform.Find("Text_position").GetComponent<Text>();
        if(Text_position == null)
        {
            Debug.Log("Text_position is null");
        }
        Text_position.text = "position";

        Text_enemyType = transform.Find("Text_enemyType").GetComponent<Text>();
        if(Text_enemyType == null)
        {
            Debug.Log("Text_enemyType is null");
        }
        Text_enemyType.text = "enemyType";

        Text_enemyColor = transform.Find("Text_enemyColor").GetComponent<Text>();
        if(Text_enemyColor == null)
        {
            Debug.Log("Text_enemyColor is null");
        }
        Text_enemyColor.text = "enemyColor";

        position_x = transform.Find("Position_x").GetComponent<InputField>();
        if(position_x == null)
        {
            Debug.Log("position_x is null");
        }

        position_y = transform.Find("Position_y").GetComponent<InputField>();
        if(position_y == null)
        {
            Debug.Log("position_y is null");
        }

        enemyType = transform.Find("EnemyType").GetComponent<InputField>();
        if(enemyType == null)
        {
            Debug.Log("enemyType is null");
        }

        enemyColor = transform.Find("EnemyColor").GetComponent<InputField>();
        if(enemyColor == null)
        {
            Debug.Log("enemyColor is null");
        }

        create = transform.Find("Create").GetComponent<Button>();
        if(create == null)
        {
            Debug.Log("create is null");
        }

        create.onClick.AddListener(CreateEnemy);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
