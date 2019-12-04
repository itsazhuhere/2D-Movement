using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
	[SerializeField] public float moveSpeed = 2.0F;
	[SerializeField] public float leftBound = -10.0F;
	[SerializeField] public float rightBound = 10.0F;

	public bool movingLeft;
	private Rigidbody2D bossBody;
	//Simple back and forth movement


    // Start is called before the first frame update
    void Start()
    {
    	bossBody = GetComponent<Rigidbody2D>();


        bossBody.velocity = new Vector2(moveSpeed, 0);
        movingLeft = false;
        bossBody.MovePosition(new Vector3(rightBound, bossBody.position.y, 0));

        Debug.Log(leftBound);
        Debug.Log(rightBound);
    }

    // Update is called once per frame
    void Update()
    {
    	//Debug.Log(bossBody.position.x);
        if(movingLeft && bossBody.position.x <= leftBound){
        	//start moving right at the boss's move speed
        	Debug.Log("switchRight");
        	bossBody.velocity = new Vector2(moveSpeed, 0);
        	movingLeft = false;
        }
        else if(!movingLeft && bossBody.position.x >= rightBound){
        	//start moving right at the boss's move speed
        	Debug.Log("switchLeft");
        	bossBody.velocity = new Vector2(-moveSpeed, 0);
        	movingLeft = true;
        }
    }
}
