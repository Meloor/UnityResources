using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket {
    void Start()
    {
        m_transform = this.transform;
        m_speed = 5.0f;
        m_livetime = 5.0f;
        Destroy(this.gameObject, m_livetime);//一定时间后销毁子弹
    }
    void Update()
    {
        //额外功能，子弹实时追踪玩家  
        //实时获取玩家飞机的位置信息    
        GameObject m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player != null) { 
            Vector3 relativePos = m_player.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(-relativePos);
         }
        m_transform.Translate(new Vector3(0.0f, 0.0f, -m_speed * Time.deltaTime));
    }

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        //如果与玩家飞机发生碰撞，则销毁子弹。
        if (other.tag.CompareTo("Player") == 0|| other.tag.CompareTo("PlayerRocket") == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
