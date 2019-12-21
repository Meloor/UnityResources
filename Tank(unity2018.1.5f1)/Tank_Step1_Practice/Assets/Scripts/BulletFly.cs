
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {
    private GameObject bulletShooter;
    private GameObject supprtPoint;
    private Rigidbody rd;
    public float modifer = 25.0f;
	// Use this for initialization
	void Start () {
        bulletShooter = GameObject.Find("bulletShooter");
        supprtPoint = GameObject.Find("supportPoint");
        rd = this.GetComponent<Rigidbody>() as Rigidbody;
        rd.velocity = (bulletShooter.transform.position - supprtPoint.transform.position) * modifer;
        Destroy(this.gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //和射线碰撞检测有关
        RaycastHit hit;
        Vector3 derect = new Vector3();
        float r = 1.0f;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
        {
            if (hit.collider.gameObject.tag == "brickTag")
            {
                for (float i = 0; i < 2 * Mathf.PI; i += 0.1f)
                    for (float j = 0; j < 2 * Mathf.PI; j += 0.1f)
                    {
                        float x = r * Mathf.Cos(i) * Mathf.Cos(j);
                        float y = r * Mathf.Sin(i);
                        float z = r * Mathf.Cos(i) * Mathf.Sin(j);
                        derect = new Vector3(x, y, z);
                        if (Physics.Raycast(transform.position, derect, out hit, 1.0f))
                        {
                            if (hit.collider.gameObject.tag == "brickTag")
                            {
                                Destroy(this.gameObject);
                                Destroy(hit.collider.gameObject);
                            }
                        }
                    }

            }
        }
	}
}
