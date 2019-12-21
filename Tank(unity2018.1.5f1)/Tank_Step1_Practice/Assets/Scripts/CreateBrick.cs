using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBrick : MonoBehaviour {
    //public GameObject brickPrefab;
    public Transform brickPrefab;
    // Use this for initialization
    void Start () {
        for (int y = 1; y < 5; y++)
            for (int x = -5; x < 15; x++) 
                Instantiate(brickPrefab, new Vector3(x, y, 5), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
