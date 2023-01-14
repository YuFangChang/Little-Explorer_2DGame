using UnityEngine;
using System.Collections;

public class playerWeapons : MonoBehaviour {
	
	//animation of attack
	public Sprite[] attack;
	public float attackFrameRate = 8.0f;
	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;

	
	public GameObject bullets;
	public float bulletsDamage = 1.0f;	
	public float bulletsSpeed = 20.0f;
	public float bulletsFirerate = 0.3f;
	public Transform spawnPosition; 	
	public AudioClip bulletSound;
	
	private float bulletCounter = 0.0f;

	private float bulletPos = 0.0f;
	private GameObject currentBullet;
	private float currentSpeed;
	private float currentDamage;
	private float fireRate = 0.25f;
	private bool dead = false;

	void Start () {
		updateBulletType();
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
	
		bulletCounter += Time.deltaTime;
		
		if(!dead){
			if(Input.GetKey(KeyCode.Space)){
				if(bulletCounter > fireRate){
					shootBullet();
				}
		 counter += Time.deltaTime*attackFrameRate;
			if(counter > i && i < attack.Length){
				rend.sprite = attack[i];
				i += 1;
			}
		
			if(counter > attack.Length){
				counter = 0.0f;
				i = 0;
			 }
			}
		}
	}
	
	void shootBullet () {
		
		Vector3 pos = new Vector3(bulletPos + transform.position.x , transform.position.y ,  transform.position.z);
		GameObject bulletPrefab = Instantiate(currentBullet, pos, Quaternion.Euler(0,180,0)) as GameObject;
		bulletPrefab.SendMessage("getDamageAmount", currentDamage, SendMessageOptions.DontRequireReceiver);
	
		GetComponent<AudioSource>().PlayOneShot(bulletSound);
		
		if(spawnPosition.position.x > transform.position.x){
			bulletPrefab.transform.GetComponent<Rigidbody>().velocity = new Vector3(currentSpeed,0,0);
		}else{
			bulletPrefab.transform.GetComponent<Rigidbody>().velocity =  new Vector3(-currentSpeed,0,0);
		}
		bulletCounter = 0.0f;
	}
	
	void updateBulletType () {
		currentBullet = bullets;
		fireRate = bulletsFirerate;
		currentSpeed = bulletsSpeed;
		currentDamage = bulletsDamage;
	}
	
	
	void died () {
		dead = true;
	}
}
