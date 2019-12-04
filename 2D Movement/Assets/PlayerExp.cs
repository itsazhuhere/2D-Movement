using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerExp : MonoBehaviour
{
	[SerializeField] public int level = 1;
	[SerializeField] public int levelUp = 100;
	[SerializeField] public float levelUpScaling = 1.5F;
	[SerializeField] public int currentExp;
	[SerializeField] public Slider expBar;
	[SerializeField] public Text levelUI;

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
        //testing
    	testExp++;
    	if(testExp >= 10){
    		testExp = 0;
    		GainExp(10);
    	}
    }

    void GainExp(int amount){
    	currentExp += amount;

    	//check for levelup
    	if(currentExp >= levelUp){
    		currentExp = currentExp - levelUp;
    		//can trigger level up events here

    		level += 1;
    		levelUp = (int) (levelUp * levelUpScaling);
    		expBar.maxValue = levelUp;
    		levelUI.text = level.ToString();
    	}

    	expBar.value = currentExp;


    }
}
