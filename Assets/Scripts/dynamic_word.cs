using System.Collections;
using UnityEngine;

public class dynamic_word : MonoBehaviour {

    public GameObject speechCloud;

    void start(){
        speechCloud.SetActive(false);  //false to hide
    }

   	public void OnTriggerEnter (Collider other){
		if(other.tag == "Player"){
			speechCloud.SetActive(true);  //true to show
		}
	}

   public void OnTriggerExit (Collider other){
            if(other.tag == "Player"){
                speechCloud.SetActive(false);  //false to hide
            }
        }

}
