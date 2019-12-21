using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
    public float m_float = 6.0f;//移动速度
    public float m_life = 3.0f;
    protected Transform m_transform;
    public GameObject m_rocket;//接收子弹预设体
    public float m_rocketTimer = 0.0f;//控制子弹发射频率


    private Transform m_ShooterLeft;//用于接受枪口空对象
    private Transform m_ShootrRight;

    private AudioSource m_audio;//接收声源组件
    public AudioClip m_fireClip;//子弹射击音效
    public Transform m_explosion;//爆炸特效

    public LayerMask m_inputMask;//接收Reference Layer
    public Vector3 m_targetPos;//接收鼠标确定的新坐标
    // Use this for initialization
    void Start()
    {
        //通过this访问比较慢，后面直接通过m_transform
        m_transform = this.transform;
        m_ShooterLeft = GameObject.Find("ShooterLeft").transform;//获取枪口位置
        m_ShootrRight = GameObject.Find("ShooterRight").transform;

        //m_audio = this.GetComponent<AudioSource>();//获取声源组件
        m_audio = this.gameObject.AddComponent<AudioSource>();//添加声源组件

        Cursor.visible = false;

    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        float moveVertical = 0.0f;//垂直方向移动距离
        float moveHonrizontal = 0.0f;//水平方向移动距离
        float move_speed = m_float * Time.deltaTime;//获取每帧移动的距离
        
        if (Input.GetKey(KeyCode.W))//按下W键向前移动    
            moveVertical -= move_speed;//正负要观察Player对象的z轴方向
        if (Input.GetKey(KeyCode.A))        
            moveHonrizontal += move_speed;       
        if (Input.GetKey(KeyCode.S))        
            moveVertical += move_speed;
        if (Input.GetKey(KeyCode.D))       
            moveHonrizontal -= move_speed;
        //实际的移动函数，y轴方向是不动的
        m_transform.Translate(new Vector3(moveHonrizontal, 0.0f, moveVertical));
        
        m_rocketTimer -= Time.deltaTime;//每帧递减，减到0就可以发射子弹了

        if (m_rocketTimer <= 0)//时间间隔还未到就不能发射子弹
        {
            m_rocketTimer = 0.1f;//重置时间间隔    
            //空格键或鼠标左键发射子弹
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                //利用空对象的位置实例化子弹
                Instantiate(m_rocket, m_ShooterLeft.position, m_ShooterLeft.rotation);
                Instantiate(m_rocket, m_ShootrRight.position, m_ShootrRight.rotation);
                //利用修改欧拉角实例化侧边子弹
                GameObject left = Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                left.transform.eulerAngles = new Vector3(0, -60, 0);
                GameObject right = Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                right.transform.eulerAngles = new Vector3(0, 60, 0);

                m_audio.PlayOneShot(m_fireClip);//射击音效
            }
            if (Input.GetMouseButton(1))//右键发射环形子弹
            {
                int num = 36;
                for(float i = 0; i < num; i++)
                {
                     GameObject temp = Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                    temp.transform.eulerAngles = new Vector3(0, 360*i/num, 0);
                }
                m_audio.PlayOneShot(m_fireClip);//射击音效
            }

        }
    }
    void MovePlayer()//通过鼠标来控制player移动
    {
        Vector3 ms = Input.mousePosition;//获取鼠标的UI位置
        Ray ray = Camera.main.ScreenPointToRay(ms);//根据鼠标的位置沿着垂直于近远裁剪面的方向发射射线
        RaycastHit hitinfo;
        bool hit = Physics.Raycast(ray, out hitinfo, 100, m_inputMask);//发射射线
        if (hit)
            m_targetPos = hitinfo.point;
        //Debug.Log(m_targetPos);
        //匀速移动player
        Vector3 tempPos = Vector3.MoveTowards(this.m_transform.position, m_targetPos, 10 * Time.deltaTime );
        //this.m_transform.position = m_targetPos;//点击一下就会瞬移
        this.m_transform.position = tempPos;     
    }
    void OnTriggerEnter(Collider other)
    {
        //与非自身子弹(敌机或敌机子弹)发生碰撞，生命值减少，生命值为0销毁玩家飞机
        if (other.tag.CompareTo("PlayerRocket") != 0)
        {
            m_life -= 1.0f;
            GameManager.Instance.ChangeLife((int)m_life); //修改界面中的生命值    
            if (m_life <= 0)
            {
                Cursor.visible = true;
                Instantiate(m_explosion, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }                
        }
    }
}
