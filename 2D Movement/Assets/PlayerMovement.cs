using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;
	GameObject playerRef;

	public Vector3 spawnPoint = new Vector3(-5, 5, 0);
	public float speedModifier = 1.0f;
	float horizontalMove = 0f;
	float blastZone = -10.0f;
	bool jump = false;
	bool crouch = false;

	public int numBullets = 1;
	public int bulletMultiplier = 1;


	PlayerHealth hp;
	PlayerExp exp;

	void Start(){
		playerRef = this.gameObject;
		hp = playerRef.GetComponent<PlayerHealth>();
		exp = playerRef.GetComponent<PlayerExp>();
	}
	
	// Update is called once per frame
	void Update () {

		float yPos = playerRef.transform.position.y;
		if (yPos <= blastZone){
			Spawn();
			hp.TakeDamage(10);
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * speedModifier;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

	}

	public int GetShootCount(){

		return exp.bulletCount * bulletMultiplier;
	}

	public void Spawn(){
		playerRef.transform.position = spawnPoint;
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
