using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour {
    public float rotate_speed = 2500.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * rotate_speed * Vector3.down, Space.Self);
    }
}
