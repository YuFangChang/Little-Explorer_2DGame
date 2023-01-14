using UnityEngine;
using System.Collections;

public class deathAnimation : MonoBehaviour {

	public Sprite[] deathSprites;
	public float frameRate = 12.0f; //speed of playing animate
	public AudioClip deathSound;
	//set the counter
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		GetComponent<AudioSource>().PlayOneShot(deathSound);
	}
	
	void Update () {	
		counter += Time.deltaTime*frameRate;
		if(counter > i && i < deathSprites.Length){
			rend.sprite = deathSprites[i];
			i += 1;
		}
	
		if(counter > deathSprites.Length){
			Destroy(gameObject);
		}
	}
}
