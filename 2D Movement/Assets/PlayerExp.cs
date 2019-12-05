using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerExp : MonoBehaviour
{
	[SerializeField] public int level = 1;
	[SerializeField] public int levelUp = 100;
	
	[SerializeField] public int currentExp;
	[SerializeField] public Slider expBar;
	[SerializeField] public Text levelUI;

    public int bulletDamage = 5;


    public float damageScaling = 1.5F;
    public float levelUpScaling = 1.5F;
    public float healthScaling = 1.5F;

	int testExp = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0;
        expBar.maxValue = levelUp;
        expBar.value = currentExp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GainExp(int amount){
    	currentExp += amount;

    	//check for levelup
    	if(currentExp >= levelUp){
    		currentExp = currentExp - levelUp;
    		//can trigger level up events here

    		level += 1;
    		levelUp = (int) (levelUp * levelUpScaling);
            bulletDamage = (int) (bulletDamage * damageScaling);

    		expBar.maxValue = levelUp;
    		levelUI.text = level.ToString();


            //upgrade player health and refresh hp
            PlayerHealth hp = GetComponent<PlayerHealth>();


            hp.LevelUp((int) (hp.startingHealth*healthScaling));
    	}

    	expBar.value = currentExp;


    }
}
