using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;//单例模式，管理器对象的实例
    public Transform m_canvas_main;//接受UI画布
    public Transform m_canvas_gameover;//接收UI画布

    public Text m_text_score;//接收标签
    public Text m_text_best;
    public Text m_text_life;

    protected int m_score = 0;//分数具体的值
    public static int m_bestscore = 0;
    protected Player m_player;//接收玩家飞机对象

    public AudioSource m_audio;
    public AudioClip m_musicClip;
	// Use this for initialization
	void Start () {
        Instance = this;

        m_audio = this.gameObject.AddComponent<AudioSource>();//添加声源组件
        m_audio.clip = m_musicClip;
        m_audio.loop = true;
        m_audio.Play();

        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //获取标签组件
        m_text_score = m_canvas_main.Find("Text_score").GetComponent<Text>();
        m_text_best = m_canvas_main.Find("Text_best").GetComponent<Text>();
        m_text_life = m_canvas_main.Find("Text_life").GetComponent<Text>();

        //初始化标签
        m_text_score.text = string.Format("分数{0}", m_score);
        m_text_best.text = string.Format("最高分{0}", m_bestscore);
        m_text_life.text = string.Format("生命值{0}", m_player.m_life);

        //获取按钮组件
        var restart_button = m_canvas_gameover.Find("Button_restart").GetComponent<Button>();
        //给重新开始按钮绑定回调函数
        restart_button.onClick.AddListener(delegate ()
        {
            //加载游戏场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("SampleScene");
        });
        //设置游戏结束画布不可见
        m_canvas_gameover.gameObject.SetActive(false);
    }
	public void AddScore(int point)//动态修改界面分数
    {
        m_score += point;
        if (m_score > m_bestscore)
            m_bestscore = m_score;
        m_text_score.text = string.Format("分数{0}", m_score);
        m_text_best.text = string.Format("最高分{0}", m_bestscore);
    }
    public void ChangeLife(int life)//动态修改界面生命值
    {
        m_text_life.text = string.Format("生命值{0}", life);
        if(life<=0)
            m_canvas_gameover.gameObject.SetActive(true);//设置游戏结束画布可见
    }
	// Update is called once per frame
	void Update () {
		
	}
}
