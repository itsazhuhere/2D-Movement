using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatLogic : MonoBehaviour
{

	public float time=0f;
	public int deaths=0;
	public int damage=0;
	public int bulletCount=0;
	public Text statText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        deaths=0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        statText.text = $"Deaths: {deaths}\nTime: {time:0.##} seconds\nDamage: {damage}\nBullets: {bulletCount}";
    }

    public void AddDeath(){
    	deaths++;
    }

    public void UpdateDamage(int newDamage){
    	damage = newDamage;
    }

    public void UpdateBulletCount(int newBulletCount){
    	bulletCount = newBulletCount;
    }
}
