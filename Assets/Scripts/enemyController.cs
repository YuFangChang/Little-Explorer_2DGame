using System.Collections;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    
	public float Speed = 4.0f;

	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private GameObject target;
	private float frameRate = 8.0f;
	private SpriteRenderer rend;
	private float origX;
	private Vector3 vel;

    // AI of enemy
    void Start()
    {
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();

		//find the player and move towards the player.
		target = GameObject.Find("Player");
		Physics.IgnoreCollision(target.GetComponent<Collider>(), GetComponent<Collider>());
		origX = transform.localScale.x; //movement is on X
		
		//ignore other enemies
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject en in enemies)  {
			if (en.GetComponent<Collider>() != GetComponent<Collider>()) {
				Physics.IgnoreCollision(GetComponent<Collider>(), en.GetComponent<Collider>());
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        //gravity
		if(!controller.isGrounded){
			vel.y -= Time.deltaTime*80;
		}else{
			vel.y = -1;
		}
		//check distance between enemy and player
		float distance = target.transform.position.x - transform.position.x;
		float ydistance = target.transform.position.y - transform.position.y;
		if(distance < 0){
			distance *= -1;
		}
		if(ydistance < 0){
			ydistance *= -1;
		}
		if(target.transform.position.x > transform.position.x){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
		}
		if(target.transform.position.x < transform.position.x){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
		}
		//if distance between 5 and 15 
		if (distance < 15 && ydistance < 5) {
			counter += Time.deltaTime * frameRate;
				//player to the left, set -Speed
				if (target.transform.position.x < transform.position.x) {
					vel.x = -Speed;
				}
				//player to the right, set Speed
				if (target.transform.position.x > transform.position.x) {
					vel.x = Speed;
				}
		}
		
		if(transform.position.y < -10){
			Destroy(gameObject);
		}
		//apply movement
		controller.Move(vel*Time.deltaTime);
	}
    
}
