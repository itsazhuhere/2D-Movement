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
    public float lootChance = 0.1f;
    public float powerUpChance = 0.8f; //right now we have a binary relationship between power ups and weapon drops
    int testDamage = 0;

    public Transform powerUpSpawn;

    List<string> powerUpNames = new List<string>(){"invincible", "speed", "bullet"};


    [SerializeField] public GameObject powerUpPrefab;



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
        

    }

    public void DropLoot(){
        //certain chance of dropping loot
        float randVal = Random.value;
        if (randVal >= lootChance){
            return;
        }

        //dropping loot
        GameObject playerRef = this.gameObject;

        //determining if power up or weapon
        randVal = Random.value;
        if(randVal < powerUpChance){
            //dropping powerup
            int powerUpNum = Random.Range(0,3);
            GameObject pUp = Instantiate(powerUpPrefab);
            PowerUpLogic logic = pUp.GetComponent<PowerUpLogic>();
            logic.SetPosition(powerUpSpawn);
            logic.powerUpName = powerUpNames[powerUpNum];

        }
        else{
            //dropping weapon
        }

        Debug.Log("drop");

    }

    public void TakeDamage(int amount){

    	currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        healthValue.text = currentHealth.ToString();
    }
}
