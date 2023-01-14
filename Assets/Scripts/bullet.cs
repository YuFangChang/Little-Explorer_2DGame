using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	public float bulletLife = 1.0f;
	private float lifeCounter = 0.0f;
	private float damage;

	
	void Update () {

		lifeCounter += Time.deltaTime;
		if(lifeCounter > bulletLife){
			Destroy(gameObject);
		}
	}
	
	void getBulletDamage (float amount) {
		damage = amount;
	}
	
	void OnTriggerEnter (Collider other){
		if(other.tag == "enemy"){
			other.SendMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}

		if(other.tag == "object"){
			Destroy(gameObject);
		}
	}
}
