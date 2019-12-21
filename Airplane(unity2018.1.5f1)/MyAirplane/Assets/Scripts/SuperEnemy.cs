using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : Enemy{
    public GameObject m_rocket;//用于接收子弹预设体
    protected Transform m_player;//用于获取玩家飞机的位置
    public float m_rocketTimer = 0.0f;//控制子弹发射频率

    //new public int m_point = 50;//隐藏父类成员变量
    //new public float m_life = 10.0f;
    void Awake()//这个函数调用比start还早
    {
        //获取玩家飞机对象
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            m_player = obj.transform;
        m_point = 50;
        m_life = 10;
    }
    override protected void UpdateMove()
    {
        m_rocketTimer -= Time.deltaTime;
        if (m_rocketTimer <= 0) {
            m_rocketTimer = 2.0f;//重新计时
            if (m_player != null)
            {
                //子弹朝着玩家飞机方向发射
                //向量坐标的运算，终点指向起点，这个方向是主角指向子弹。
                //但是Rocket.cs中子弹是沿着其-Z轴移动的，所以这里子弹会向后飞，所以要反过来
                Vector3 relativePos = m_player.position - m_transform.position;
                Instantiate(m_rocket, m_transform.position, Quaternion.LookRotation(-relativePos));
            }
        }
        this.transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));   
    }
}
