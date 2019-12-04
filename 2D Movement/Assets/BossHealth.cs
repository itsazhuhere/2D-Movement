using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
	[SerializeField] public int startingHealth = 100000;                            // The amount of health the player starts the game with.
    [SerializeField] public int currentHealth;                                   // The current health the player has.
    [SerializeField] public Slider healthSlider;
    [SerializeField] public Text healthValue;

    int testDamage = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing
    	testDamage += 1;
    	if(testDamage >= 10){
    		testDamage = 0;
    		TakeDamage(1000);
    	}

    }

    void TakeDamage(int amount){

    	currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        healthValue.text = currentHealth.ToString();
    }
}
