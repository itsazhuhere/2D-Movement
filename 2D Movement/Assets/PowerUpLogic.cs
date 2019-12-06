using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
	public string powerUpName;
	public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Transform startPoint){
    	this.transform.position = startPoint.position;
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == playerTag){
    		Debug.Log("powerUp");

    		PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
    		powerUp.TriggerPowerUp(powerUpName);

    		Destroy(gameObject);
    	}
    }
}
