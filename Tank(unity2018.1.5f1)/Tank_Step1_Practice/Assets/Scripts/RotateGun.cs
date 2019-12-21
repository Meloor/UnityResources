using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour {
    public Transform supportPoint;
    public float modifier = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(supportPoint.position, supportPoint.up,Input.GetAxis("Mouse Y")* modifier);
            transform.RotateAround(supportPoint.position, supportPoint.right, Input.GetAxis("Mouse X") * modifier);
        }
	}
}
