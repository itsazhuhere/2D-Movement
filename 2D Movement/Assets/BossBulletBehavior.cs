using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletBehavior : MonoBehaviour
{

	[SerializeField] public CircleCollider2D hitDetect;
	[SerializeField] public Rigidbody2D bulletBody;
	public GameObject playerRef;


	[SerializeField] Vector2 direction;
	[SerializeField] float speed = 1.0F;
	public float playerBulletSpeed = 5.0F;
	public int bulletDamage = 5;
	public int hitExp = 50;


	public string playerTag = "Player";
	public string bossTag = "Boss";
	public string bullet = "Bullet";


	public bool isPlayer = false;
	public string origin;

    public Sprite playerBullet;
    public Sprite rocket;
    public Sprite laser;
    SpriteRenderer sprite;

    public Transform target;
    bool isHoming = false;

    string bulletName="";



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Boss").transform;
    	bulletBody = GetComponent<Rigidbody2D>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    	if(isPlayer){
    		origin = playerTag;
    		speed = playerBulletSpeed;
            if(bulletName == "rocket"){
                sprite.sprite = rocket;
            }
            else if(bulletName == "laser"){
                sprite.sprite = laser;
            }
            else{
                sprite.sprite = playerBullet;
            }
            

            //tsting
    	}
    	else{
    		origin = bossTag;
            sprite.color = Color.red;
            sprite.sortingOrder = 1;
    	}


    	playerRef = GameObject.FindWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHoming){
            direction = target.position - transform.position;
            direction = direction.normalized;
            float angle = AngleOffAroundAxis(Vector3.up, direction, Vector3.forward);
            transform.Rotate(0f,0f,angle);
        }
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

    public void SetBullet(string name){
        
        if(name == "rocket"){
            if(sprite!=null){
                sprite.sprite = rocket;
            }
            else{
                bulletName = "rocket";
            }
            
            isHoming = true;
        }
        else if(name == "laser"){
            if(sprite!=null){
                sprite.sprite = laser;
            }
            else{
                bulletName = "laser";
            }
            isHoming = false;
        }

        else{
            if(sprite!=null){
                sprite.sprite = rocket;
            }
            else{
                bulletName = "bullet";
            }
            isHoming = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
    	if (other.tag == origin){
    		return;
    	}
    	if(isPlayer){
    		if(other.tag == bossTag){
	    		//Debug.Log("hit");

	    		BossHealth hp = other.gameObject.GetComponent<BossHealth>();
	    		//give the player exp too
	    		PlayerExp exp = playerRef.GetComponent<PlayerExp>();
	    		hp.TakeDamage(exp.bulletDamage * bulletDamage);
	    		exp.GainExp(hitExp);

	    		Destroy(gameObject);
	    	}
    	}
    	else{
    		if(other.tag == playerTag){
	    		PlayerHealth hp = other.gameObject.GetComponent<PlayerHealth>();
	    		hp.TakeDamage(bulletDamage);
	    		Destroy(gameObject);
	    	}
    	}
    	if(other.tag != bullet){
            if(isPlayer && other.tag == "Ground"){
                return;
            }
    		Destroy(gameObject);
    	}
    	
    	
    }

    public void Move(Vector2 initDirection, float angle=0f)
    {
        //rotate the image so that it is oriented correctly too
        transform.Rotate(0f,0f,angle);
    	direction = initDirection.normalized;
    }

    void FixedUpdate(){
    	bulletBody.MovePosition(bulletBody.position + speed * direction * Time.fixedDeltaTime);
    }
}
