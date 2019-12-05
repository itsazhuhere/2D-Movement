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
	public int hitExp = 5;


	public string playerTag = "Player";
	public string bossTag = "Boss";
	public string bullet = "Bullet";


	public bool isPlayer = false;
	public string origin;

    // Start is called before the first frame update
    void Start()
    {
    	bulletBody = GetComponent<Rigidbody2D>();
    	if(isPlayer){
    		origin = playerTag;
    		speed = playerBulletSpeed;
    	}
    	else{
    		origin = bossTag;
    	}


    	playerRef = GameObject.FindWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other){
    	if (other.tag == origin){
    		return;
    	}
    	if(isPlayer){
    		if(other.tag == bossTag){
	    		Debug.Log("hit");
	    		BossHealth hp = other.gameObject.GetComponent<BossHealth>();
	    		//give the player exp too
	    		PlayerExp exp = playerRef.GetComponent<PlayerExp>();
	    		hp.TakeDamage(exp.bulletDamage);
	    		exp.GainExp(hitExp);

	    		Destroy(gameObject);
	    	}
    	}
    	else{
    		if(other.tag == playerTag){
	    		Debug.Log("hit");
	    		PlayerHealth hp = other.gameObject.GetComponent<PlayerHealth>();
	    		hp.TakeDamage(bulletDamage);
	    		Destroy(gameObject);
	    	}
    	}
    	if(other.tag != bullet){
    		Destroy(gameObject);
    	}
    	
    	
    }

    public void Move(Vector2 initDirection)
    {
    	direction = initDirection;
    }

    void FixedUpdate(){
    	bulletBody.MovePosition(bulletBody.position + speed * direction * Time.fixedDeltaTime);
    }
}
