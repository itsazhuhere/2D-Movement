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
    public float lootChance = 0.99f;    
    public float powerUpChance = 0.1f; //right now we have a binary relationship between power ups and weapon drops
    int testDamage = 0;

    bool triggeredDeath=false;

    public Transform powerUpSpawn;
    SpriteRenderer sprite;
    public Sprite deathSprite;

    List<string> powerUpNames = new List<string>(){"invincible", "speed", "bullet"};


    [SerializeField] public GameObject powerUpPrefab;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void BossLevelUp(){

    }

    public void DropLoot(){
        //certain chance of dropping loot
        float randVal = Random.Range(0f,1f);
        Debug.Log(randVal);
        Debug.Log(lootChance);
        if (randVal >= lootChance){
            return;
        }

        //dropping loot
        GameObject playerRef = this.gameObject;

        //determining if power up or weapon
        randVal = Random.Range(0f,1f);
        if(randVal < powerUpChance){
            Debug.Log("powerup");
            //dropping powerup
            int powerUpNum = Random.Range(0,3);
            GameObject pUp = Instantiate(powerUpPrefab);
            PowerUpLogic logic = pUp.GetComponent<PowerUpLogic>();
            logic.SetPosition(powerUpSpawn);
            logic.SetPowerUp(powerUpNames[powerUpNum]);

        }
        else{
            Debug.Log("weapon");
            //dropping weapon
            //hard coding values because no time
            string weaponName;
            int powerUpNum = Random.Range(0,3);
            if(powerUpNum == 0){
                //giant bullet gun
                weaponName = "giant";
            }
            else if(powerUpNum == 1){
                weaponName = "rocket";
            }
            else{
                weaponName = "laser";
            }
            GameObject pUp = Instantiate(powerUpPrefab);
            PowerUpLogic logic = pUp.GetComponent<PowerUpLogic>();
            logic.SetPosition(powerUpSpawn);
            logic.SetPowerUp(weaponName);

        }


    }

    public void TakeDamage(int amount){
        DropLoot();

    	currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        healthValue.text = currentHealth.ToString();

        if(currentHealth <= 0 && !triggeredDeath){
            triggeredDeath = true;
            sprite.sprite = deathSprite;
            sprite.color = Color.white;
        }
    }
}
