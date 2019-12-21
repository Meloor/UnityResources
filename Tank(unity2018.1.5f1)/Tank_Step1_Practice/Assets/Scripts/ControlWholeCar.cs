using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWholeCar : MonoBehaviour {
    public float move_speed = 1.57f;
    public float rotate_speed =120.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * move_speed * Vector3.forward,Space.Self);
        transform.Rotate(Input.GetAxis("Horizontal") * Time.deltaTime * rotate_speed * Vector3.up, Space.Self);
    }
}
