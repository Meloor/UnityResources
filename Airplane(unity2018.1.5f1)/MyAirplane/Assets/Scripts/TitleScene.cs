using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	public void OnButtonSartGame()
    {
        //加载到游戏场景
        SceneManager.LoadScene("SampleScene");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
