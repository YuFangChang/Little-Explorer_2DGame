using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {

	public string nextLevel;
	
	void Start () {
		PlayerPrefs.SetString("savedLevel", SceneManager.GetActiveScene().name);
	}
	
	//if hit by Player, go to next level
	void OnTriggerEnter (Collider other){
		if(other.tag == "Player"){
			SceneManager.LoadScene(nextLevel);
		}
	}

}
