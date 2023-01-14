using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {
	
	public int hearts = 6;
	public AudioClip hitSound;  //when Player hit something
	public GameObject deathAnim;

	public Sprite full;  //3 type of heart
	public Sprite half;
	public Sprite empty;

	public GameObject youdiedtext;

	public AudioClip heartSound;
	
	private bool dead = false;
	private bool canGetHurt = true;
	//renderer 2d sprite
	private SpriteRenderer rend;
	
	private Image[] heartsGUI; 
	private int health;
	
	void Start () {
		
		health = hearts*2;
	
		GameObject getHearts = GameObject.Find("Canvas/hearts");
	
		getHearts.SendMessage("addHearts", hearts, SendMessageOptions.DontRequireReceiver);
		Image[] allChildren = getHearts.GetComponentsInChildren<Image>();
		heartsGUI = new Image[allChildren.Length];
		health = allChildren.Length*2;
		for(int i = 0;i < allChildren.Length;i++){
			heartsGUI[i] = allChildren[i] as Image;
		}
		rend = GetComponent<SpriteRenderer>();
	}

	void takeDamage (int amount) {
		if(canGetHurt && !dead){
			canGetHurt = false;
			GetComponent<AudioSource>().PlayOneShot(hitSound);
			health -= amount;
			StartCoroutine(checkHealth());
			StartCoroutine(resetCanHurt());
		}
	}
	
	void OnTriggerStay (Collider other){
		if(other.tag == "enemy" || other.tag == "trap"){
			if(canGetHurt && !dead){
				canGetHurt = false;
				GetComponent<AudioSource>().PlayOneShot(hitSound);
				health -= 1;
				StartCoroutine(checkHealth());
				StartCoroutine(resetCanHurt());
			}
		}
		//if pick the heart, health will +1
		if(other.GetComponent<Collider>().tag == "heart"){
			Destroy(other.gameObject);
			addHealth();
		}
	}
	
	void OnCollisionStay (Collision other){
		if(other.collider.tag == "enemy" || other.collider.tag == "trap"){
			if(canGetHurt && !dead){
				canGetHurt = false;
				GetComponent<AudioSource>().PlayOneShot(hitSound);
				health -= 1;
				StartCoroutine(checkHealth());
				StartCoroutine(resetCanHurt());
			}
		}
	}
	
	public IEnumerator resetCanHurt () {
		rend.color = new Vector4(1.0f,0.5f,0.5f,1.0f); //red
		yield return new WaitForSeconds(1f);
		rend.color = new Vector4(1.0f,1.0f,1.0f,1.0f); //white
		canGetHurt = true;
	}
	
	public IEnumerator checkHealth () {
		updateHearts();
		if(health <= 0 && dead == false){
			dead = true;
			youdiedtext.SetActive(true);
			Instantiate(deathAnim, transform.position, Quaternion.Euler(0,180,0));
			BroadcastMessage("died", SendMessageOptions.DontRequireReceiver);
			var rend = GetComponent<SpriteRenderer>();
			rend.enabled = false;
			//wait to restart: 2 seconds
			yield return new WaitForSeconds(2);
			//reload the level
	        string level = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(level);
		}
	}
	
	//pick a heart will add health
	void addHealth () {
		GetComponent<AudioSource>().PlayOneShot(heartSound);
		health += 1;
		if(health > 6)
		{
			health = 6;
		}
		updateHearts();
	}
	
	//update the Health System on screen
	void updateHearts () {	
		for(int i = 0;i < heartsGUI.Length;i++){
			int check = (i+1)*2;
			//if heart is full
			if(check < health+1 ){
				heartsGUI[i].sprite = full;
			}
			//if heart is half
			if(check == health+1 ){
				heartsGUI[i].sprite = half;
			}
			//if heart is empty
			if(check > health+1 ){
				heartsGUI[i].sprite = empty;
			}

		}
	}
}
