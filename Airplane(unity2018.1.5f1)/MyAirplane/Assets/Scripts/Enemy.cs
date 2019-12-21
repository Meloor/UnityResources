using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float m_speed = 1.0f;//向前（Z轴）移动的速度
    public float m_life = 5.0f;//生命值
    protected float m_rotSpeed = 30.0f;
    private float daltaZ = 0.0f;//向z轴移动的总距离
    protected Transform m_transform;

    private Renderer m_renderer;//接收渲染器组件
    public bool m_isActive=false;//标志是否已激活

    public Transform m_explosion;//爆炸特效

    public int m_point = 10;//分数
     // Use this for initialization
    void Start () {
        m_transform = this.transform;
        m_renderer = this.GetComponent<Renderer>();//获取渲染器组件
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMove();
        //如果已经被激活，且是不可见的，销毁对象。
        //m_isActive条件的判断是为了防止一开始就创建在不可见的地方
        //还没变得可见就被销毁了
        if (m_isActive && !m_renderer.isVisible)
            Destroy(this.gameObject);
        
	}
    void OnBecameVisible()//当对象变得可见时调用该函数
    {
        //当敌机变得可见时，将m_isActive设置为true
        m_isActive = true;
    }
    protected virtual void UpdateMove()
    {
        //使用距离作为sin的自变量
        //daltaZ += m_speed * Time.deltaTime;
        //float rx = 1.0f * Mathf.Sin(daltaZ)*Time.deltaTime;//x轴偏移是一个距离的正弦值
        //Translate函数移动位置是相对的，所以不能用daltaz这个累加值
        //m_transform.Translate(new Vector3(rx, 0, m_speed * Time.deltaTime));

        //使用时间作为sin的自变量
        float rx = 1.0f * Mathf.Sin(Time.time) * Time.deltaTime;
        m_transform.Translate(new Vector3(rx, 0, m_speed * Time.deltaTime));
    }
    void OnTriggerEnter(Collider other)
    {
        //与玩家子弹碰撞，生命值减少，生命值为0销毁敌机
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
                m_life -= rocket.m_power;
            if (m_life <= 0)
            {
                Instantiate(m_explosion, m_transform.position, Quaternion.identity);
                //GameManager.Instance.AddScore(m_point);//销毁一个敌机，加一分    
                Destroy(this.gameObject);
            }
        }//与玩家飞机碰撞，直接销毁敌机
        else if (other.tag.CompareTo("Player") == 0)
        {
            m_life = 0;
            Instantiate(m_explosion, m_transform.position, Quaternion.identity);
            //GameManager.Instance.AddScore(m_point);//销毁一个敌机，加一分
            Destroy(this.gameObject);
        }
        if (m_life <= 0)//销毁后为什么还会调用这个代码
        {
            GameManager.Instance.AddScore(m_point);//销毁一个敌机,加对应分数
            //GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(m_point);
        }
            
    }

}

