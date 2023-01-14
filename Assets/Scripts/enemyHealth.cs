using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {

	public GameObject deathAnim;
	public GameObject heartDrop;
	public int health = 6;
	public AudioClip hurtSound;
	
	private SpriteRenderer rend;
	private bool isDead = false;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
	}
	
	//when the bullet hit the enemy...
	void takeDamage (float damage) {
		if(!isDead){
			GetComponent<AudioSource>().PlayOneShot(hurtSound);
			health--;
			//print(health);
			//print(gameObject.name);
			if(health <= 0){
				isDead = true;
				//play the death animation
				Instantiate(deathAnim, transform.position, Quaternion.Euler(0,180,0));
				//set the probability of dropping heart
				int randNum = Random.Range(1,3);
				if(randNum == 2 || randNum == 3){
					Instantiate(heartDrop, transform.position, Quaternion.Euler(0,180,0));
				}
				Destroy(gameObject);
			}else{
				//enemy is not dead
				StartCoroutine(resetColor());
			}
		}
	}
	
	//same as player
	public IEnumerator resetColor () {
		rend.color = new Vector4(1.0f,0.25f,0.25f,1.0f);
		yield return new WaitForSeconds (0.125f);
		rend.color = new Vector4(1.0f,1.0f,1.0f,1.0f);
	}
}
