﻿using System.Collections;
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
    public Text eventDisplay;
    EventScript eventScript;

    public int bulletDamage = 5;

    public int bulletCount = 1;
    public int bulletUpgrade = 3;


    public float damageScaling = 1.5F;
    public float levelUpScaling = 1.5F;
    public float healthScaling = 1.5F;

    public Text statDisplay;
    StatLogic statLogic;

	int testExp = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0;
        expBar.maxValue = levelUp;
        expBar.value = currentExp;
        eventScript = eventDisplay.GetComponent<EventScript>();

        statLogic = statDisplay.GetComponent<StatLogic>();

        statLogic.UpdateDamage(bulletDamage);
        statLogic.UpdateBulletCount(bulletCount);
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
            statLogic.UpdateDamage(bulletDamage);
            if(level % bulletUpgrade == 0){
                bulletCount += 1;
                statLogic.UpdateBulletCount(bulletCount);
            }

    		expBar.maxValue = levelUp;
    		levelUI.text = level.ToString();
            eventScript.TriggerLevelUp();


            //upgrade player health and refresh hp
            PlayerHealth hp = GetComponent<PlayerHealth>();


            hp.LevelUp((int) (hp.startingHealth*healthScaling));
    	}

    	expBar.value = currentExp;


    }
}
