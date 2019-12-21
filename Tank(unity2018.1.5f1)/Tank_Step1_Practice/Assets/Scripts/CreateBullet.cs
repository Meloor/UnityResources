using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject wildFire;
    public GameObject bulletShooter;
    public AudioClip bulletExplosionSound;
    private AudioSource bulletExplosion;
    // Use this for initialization
    void Start()
    {
        bulletExplosion = this.gameObject.AddComponent<AudioSource>();
        bulletExplosion.clip = bulletExplosionSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(bullet, bulletShooter.transform.position, bulletShooter.transform.rotation);
                Instantiate(wildFire, bulletShooter.transform.position, bulletShooter.transform.rotation);
                bulletExplosion.Play();
            }
        }
    }
}
