using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

	[SerializeField] public int startingHealth = 100;                            // The amount of health the player starts the game with.
    [SerializeField] public int currentHealth;                                   // The current health the player has.
    [SerializeField] public Slider healthSlider;                               // Reference to the UI's health bar.
    [SerializeField] public Text healthUI;
    [SerializeField] public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    [SerializeField] public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    [SerializeField] public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    
    bool damaged;
    bool isDead;
    float flickerInterval = 0.1F;
    float flickerTimer = 0.0F;
    bool flickering;

    int test_damage;
    int fallDamage = 10;
    public float invincibleTime = 1.0F;
    float remainingInvincibility = 0F;

    PlayerMovement spawn;
    SpriteRenderer sprite;

    public Text statBox;
    StatLogic statLogic;

    int deaths = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        isDead = false;

        test_damage = 0;

        spawn = this.gameObject.GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        flickering = false;

        statLogic = statBox.GetComponent<StatLogic>();
    }

    // Update is called once per frame
    void Update ()
    {
        float delTime = Time.deltaTime;
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
            //removing for now to make the game harder 
            //TemporaryInvincibility(invincibleTime);
            flickering = true;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * delTime);
            remainingInvincibility -= delTime;
        }

        //Ensures we only consider flickering when we are in invincibility
        if(remainingInvincibility > 0){
            if(flickering){
                sprite.enabled = false;
            }
            else{
                sprite.enabled = true;
            }
            flickerTimer -= delTime;
            if(flickerTimer <= 0){
                flickerTimer = flickerInterval;
                flickering = !flickering; //swap from display to not display or vice versa
            }
            
        }
        else{
            //Ensures we don't accidentally leave the sprite disabled after invincibility ends
            sprite.enabled = true;
        }
        // Reset the damaged flag.
        damaged = false;

    }


    public void TakeDamage (int amount, bool triggerDamage=true)
    {
        if(remainingInvincibility > 0){
            return;
        }
        // Set the damaged flag so the screen will flash.
        damaged = triggerDamage;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        healthUI.text = currentHealth.ToString();

        // Play the hurt sound effect.

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }

    public void LevelUp(int newMaxHealth, bool triggerAnimation=true){
        startingHealth = newMaxHealth;
        currentHealth = newMaxHealth;
        healthSlider.maxValue = newMaxHealth;
        TakeDamage(0, false);
    }

    public void Fall(){
        spawn.Spawn();
        TakeDamage(fallDamage);
    }

    public void TemporaryInvincibility(float time){
        remainingInvincibility = time;
    }

    public void Heal(int amount = 0, float percent=0.0f){
        if(amount > 0){
            currentHealth += amount;
            
        }
        else if(percent > 0){
            currentHealth += (int)(percent * startingHealth);
        }
        currentHealth = Mathf.Min(startingHealth, currentHealth);
        TakeDamage(0, false);
    }


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        spawn.Spawn();
        //We use LevelUp because it already has logic that sets max Hp and refreshes hp
        LevelUp(startingHealth, false);
        deaths++;
        statLogic.AddDeath();
        
        isDead = false;

    }
}
