using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShining : MonoBehaviour
{
    protected float shiningTime = 0.1f;
    public Color shiningColor = Color.red;
    private float timer = 0f;
    public bool isOpen = true;
    private bool isChange = false;

    private SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            timer += Time.deltaTime;
            if (timer > shiningTime)
            {
                Shine();
                timer = 0;
            }
        }
        //代码测试
        if (Input.GetKeyDown(KeyCode.J))
        {
            Open();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Close();
        }
    }

    private void Shine()
    {
        isChange = !isChange;
        if (isChange)
        {
            m_spriteRenderer.color = shiningColor;
        }
        else
        {
            m_spriteRenderer.color = Color.white;
        }
    }

    public void ChangeColor(Vector3 color)
    {
        shiningColor = new Color(color.x,color.y,color.z);
    }

    public void Open()
    {
        isOpen = true;
    }

    public void Close()
    {
        isOpen = false;
        //关闭同时回到初始状态
        m_spriteRenderer.color=Color.white;
    }
}
