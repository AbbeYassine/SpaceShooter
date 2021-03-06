﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {


	public float speed;
	public Boundary boundary;
	public float tilt;
	
	Rigidbody rigidbody ;


 	public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    public AudioSource audio;
    

    void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
           
        }
    }

	void Start(){
		rigidbody = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
