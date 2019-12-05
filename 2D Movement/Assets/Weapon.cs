using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	public Transform firePoint;
	float fireRate;
	float damageMultiplier;
	float cooldownTime;
	public float COOLDOWN_TIME = 1f;
	[SerializeField] GameObject bulletPrefab;
	public GameObject target;


	public bool isPlayer;

	Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = COOLDOWN_TIME;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    	if(!isPlayer){
    		Shoot();
    	}
    	else{
    		if(Input.GetKey(KeyCode.Mouse0)){
    			Shoot();
    		}
    	}
        cooldownTime -= Time.deltaTime;
    }




    void Shoot()
    {
    	if(cooldownTime <= 0)
    	{
    		GameObject go = Instantiate(bulletPrefab);
    		Vector3 direction;
    		BossBulletBehavior b = go.GetComponent<BossBulletBehavior>();
    		if(!isPlayer){
    			direction = target.transform.position - firePoint.position;
    		}
    		else{
    			Debug.Log("playerFire");
    			Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    			direction = mousePos - firePoint.position;
    			b.isPlayer = true;
    		}
    		go.transform.position = new Vector2(firePoint.position.x, firePoint.position.y);
    		

    		b.Move(direction.normalized);
    		cooldownTime = COOLDOWN_TIME;

    	}
    	


    }
}
