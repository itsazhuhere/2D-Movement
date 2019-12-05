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

    int test_damage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        isDead = false;

        test_damage = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;

    }


    public void TakeDamage (int amount, bool triggerDamage=true)
    {
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

    public void LevelUp(int newMaxHealth){
        startingHealth = newMaxHealth;
        currentHealth = newMaxHealth;
        healthSlider.maxValue = newMaxHealth;
        TakeDamage(0, false);
    }


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

    }     
}
