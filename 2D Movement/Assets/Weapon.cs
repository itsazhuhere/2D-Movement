using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	public Transform firePoint;
	float fireRate;
	float damageMultiplier = 1.0f;
	float cooldownTime;
	int numShots=1;
	public int shotMultiplier = 1;
	float playerBulletSpeed = 30.0f;
	float bulletScaling = 1.0f;
	public float COOLDOWN_TIME = 1f;
	[SerializeField] GameObject bulletPrefab;
	public GameObject target;
	public GameObject playerRef;
	public PlayerMovement move;

    public float maxArc = 90.0f;
    public float setArc = 5.0f;
    public float arcScaling = 0.5f;


    SpriteRenderer sprite;
    public Sprite laserGun;
    public Sprite defaultGun;
    public Sprite rocketLauncher;
    public Sprite giantGun;

	public bool isPlayer;

    bool laserGunSet=false;

    string bulletType;

	Camera cam;

    GameObject bossRef;
    


    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = COOLDOWN_TIME;
        cam = Camera.main;
        playerRef = GameObject.FindWithTag("Player");
        move = playerRef.GetComponent<PlayerMovement>();
        sprite = this.gameObject.transform.Find("Fire Point").GetComponent<SpriteRenderer>();
        if(isPlayer){
            //SetLaserGun();
        }

        bossRef = GameObject.FindWithTag("Boss");

        
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

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(mousePos - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

    }

    public void ChangeGuns(float damage, float cooldown, int shots, float bulletSpeed, float bulletSize, string gunName=""){
    	//weapons that have entirely different bullet/firing functionality (i.e. lasers) will have to be hard coded into the weapon class
    	//everything else can be done using value changing via this function
    	damageMultiplier = damage;
    	cooldownTime = cooldown;
    	numShots = shots;
    	playerBulletSpeed = bulletSpeed;
    	bulletScaling = bulletSize;
        laserGunSet = false;


        switch(gunName){
            case "laser":
                sprite.sprite = laserGun;
                bulletType = "laser";
                break;
            case "rocket":
                sprite.sprite = rocketLauncher;
                bulletType = "rocket";
                break;
            case "giant":
                sprite.sprite = giantGun;
                bulletType = "";
                break;
            default:
                sprite.sprite = defaultGun;
                bulletType = "";
                break;
        }
    }

    public void SetLaserGun(){
        //Separate function because I wanted to make some custom functionality but ended up scrapping the changes
        ChangeGuns(10f, 0.25f, 2, 50f, 1f, "laser");

    }


    //public void SwitchToHomingShot()
    Vector3 DirFromAngle(float angleInDegrees){
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public static float AngleOffAroundAxis(Vector3 v, Vector3 forward, Vector3 axis, bool clockwise = false)
    {
        Vector3 right;
        if(clockwise)
        {
            right = Vector3.Cross(forward, axis);
            forward = Vector3.Cross(axis, right);
        }
        else
        {
            right = Vector3.Cross(axis, forward);
            forward = Vector3.Cross(right, axis);
        }
        return Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, forward)) * Mathf.Rad2Deg;
    }

    void SpreadShot(int shots){
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction;
        direction = mousePos - firePoint.position;
        direction = direction.normalized;
        float directionAngle = AngleOffAroundAxis(Vector3.up, direction, Vector3.forward);
        Vector3 originalDirection = DirFromAngle(directionAngle);
        float adjustedArc = setArc*((float)shots/(1.0f+arcScaling));
        Vector3 leftArc = DirFromAngle(directionAngle+adjustedArc);
        Vector3 rightArc = DirFromAngle(directionAngle-adjustedArc);
        for(int i=0; i<shots;i++){
            GameObject go = Instantiate(bulletPrefab);
            go.transform.localScale *= bulletScaling;
            BossBulletBehavior b = go.GetComponent<BossBulletBehavior>();
            b.SetBullet(bulletType);
            b.bulletDamage = (int)damageMultiplier;
            
            b.isPlayer = true;
            b.playerBulletSpeed = playerBulletSpeed;
            go.transform.position = new Vector2(firePoint.position.x, firePoint.position.y);
            
            Vector3 lerpDirection = Vector3.Slerp(leftArc, rightArc, (float)i / (float)(shots-1));
            b.Move(lerpDirection, AngleOffAroundAxis(Vector3.up, lerpDirection, Vector3.back));
            //b.Move(originalDirection);
            
        }
        cooldownTime = COOLDOWN_TIME;

    }

    void Shoot()
    {
    	if(cooldownTime <= 0)
    	{
            if(laserGunSet){

            }
            else{
                int totalShots = move.GetShootCount();
                //int totalShots = 3;
                if(isPlayer && totalShots > 1){
                    SpreadShot(totalShots);
                    return;
                }
                GameObject go = Instantiate(bulletPrefab);
                go.transform.localScale *= bulletScaling;
                Vector3 direction;
                BossBulletBehavior b = go.GetComponent<BossBulletBehavior>();
                
                if(!isPlayer){
                    direction = target.transform.position - firePoint.position;
                }
                else{
                    b.SetBullet(bulletType);
                    b.bulletDamage = (int)damageMultiplier;
                    Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    direction = mousePos - firePoint.position;
                    b.isPlayer = true;
                    b.playerBulletSpeed = playerBulletSpeed;
                }
                go.transform.position = new Vector2(firePoint.position.x, firePoint.position.y);
                
                direction = direction.normalized;
                b.Move(direction, AngleOffAroundAxis(Vector3.up, direction, Vector3.back));
                
            }

            cooldownTime = COOLDOWN_TIME;

    		

    	}
    	


    }
}
