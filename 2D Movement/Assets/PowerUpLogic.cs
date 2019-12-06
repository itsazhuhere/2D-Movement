using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
	public string powerUpName;
	public string playerTag = "Player";

	public Sprite bulletImage;
	public Sprite invincibilityImage;
	public Sprite speedImage;
    public Sprite giantGun;
    public Sprite laserGun;
    public Sprite rocketGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPowerUp(string name){
    	powerUpName = name;
    	SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(name);
    	switch(powerUpName){
    		case "bullet":
    			sprite.sprite = bulletImage;
    			break;
    		case "invincible":
    			sprite.sprite = invincibilityImage;
    			break;
    		case "speed":
    			sprite.sprite = speedImage;
    			break;
            case "rocket":
                sprite.sprite = rocketGun;
                break;
            case "laser":
                sprite.sprite = laserGun;
                break;
            case "giant":
                sprite.sprite = giantGun;
                break;
    		default:
    			Debug.Log("Wrong power up name: " + name);
    			break;
    	}
    }

    public void SetPosition(Transform startPoint){
    	this.transform.position = startPoint.position;
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == playerTag){

    		PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
    		powerUp.TriggerPowerUp(powerUpName);

    		Destroy(gameObject);
    	}
    }
}
