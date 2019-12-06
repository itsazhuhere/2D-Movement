using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour
{

	public Text eventTextBox;
	public float maxTime = 5.0f;
	public float eventTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	eventTimer -= Time.deltaTime;
        if(eventTimer <= 0){
        	eventTextBox.text = "";
        }

    }
    public void SetText(string newText){
    	eventTextBox.text = newText;
    	eventTimer = maxTime;
    }

    public void TriggerLevelUp(){
    	SetText("Level Up!");
    }
}
