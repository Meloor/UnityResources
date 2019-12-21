using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour {
    public Vector3 relativePosition;
    public GameObject targetObject;
	// Use this for initialization
	void Start () {
        relativePosition = new Vector3(0.0f, -0.6f, 2.0f);
        targetObject = GameObject.Find("Car");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = targetObject.transform.position - relativePosition;
	}
}
