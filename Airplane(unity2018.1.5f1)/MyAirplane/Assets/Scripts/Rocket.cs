using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public float m_speed = 10.0f;//控制子弹飞行速度
    public float m_livetime = 1.0f;//子弹生存时间
    protected Transform m_transform;
    public float m_power = 2.0f;
    void Start()
    {
        m_transform = this.transform;
        Destroy(this.gameObject, m_livetime);//一定时间后销毁子弹
    }
    void Update()
    {
        m_transform.Translate(new Vector3(0.0f, 0.0f, - m_speed * Time.deltaTime));

    }
    void OnTriggerEnter(Collider other)
    {
        //如果碰到敌机则销毁子弹
        if (other.tag.CompareTo("Enemy") == 0|| other.tag.CompareTo("EnemyRocket") == 0)
        {
            Destroy(this.gameObject);
        }
    }
}

