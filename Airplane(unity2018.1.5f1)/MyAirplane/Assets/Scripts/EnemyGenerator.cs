using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    public Transform m_enemy;//用于接收敌人预设体
    protected Transform m_transform;
	// Use this for initialization
	void Start () {
        m_transform = this.transform;
        StartCoroutine(SpawnEnemy());//调用SpawnEnemy函数
	}
	IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));//等待5~10s再继续往下执行
        Instantiate(m_enemy, m_transform.position, m_enemy.rotation);//实例化敌人
        StartCoroutine(SpawnEnemy());//递归调用SpawnEnemy函数，实现持续生成
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "item.png", true);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
