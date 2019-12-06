using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

	public float speedBoost = 2.0f;
	public int bulletMultiplier = 2;
	float powerUpTimer = 0.0f;
	string currentPowerUp = "";
	GameObject playerRef;
	PlayerMovement move;
	PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();

    }

    public void TriggerPowerUp(string powerUpName, float powerUpTime=30.0f){

    	powerUpName = powerUpName.Trim().ToLower();
    	RemoveCurrentPowerUp();
    	currentPowerUp = powerUpName;
        Debug.Log(currentPowerUp);
    	switch(powerUpName){
    		case "speed":
    			move.speedModifier = speedBoost;
    			break;
    		case "bullet":
                move.bulletMultiplier = bulletMultiplier;
    			break;
    		case "invincible":
    			health.TemporaryInvincibility(powerUpTime);
    			break;

    		default:
    			Debug.Log("Unknown powerup: " + powerUpName);
    			currentPowerUp = "";
    			return;
    	}

    	powerUpTimer = powerUpTime;

    }

    void RemoveCurrentPowerUp(){
    	switch(currentPowerUp){
    		//defaults should always be 1.0f or equivalent
    		case "speed":
    			move.speedModifier = 1.0f;
    			break;
    		case "bullet":
                move.bulletMultiplier = 1;
    			break;
    		case "invincible":
    			health.TemporaryInvincibility(-1.0f);
    			break;

    		default:
    			Debug.Log("Update defaulted " + currentPowerUp);

    			return;
		}

        currentPowerUp = "";
    }



    // Update is called once per frame
    void Update()
    {
    	powerUpTimer -= Time.deltaTime;
        if (currentPowerUp != "" && powerUpTimer <= 0){
        	RemoveCurrentPowerUp();
        }
    }
}




