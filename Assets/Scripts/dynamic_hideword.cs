using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamic_hideword : MonoBehaviour
{
public GameObject hideWord;

   	public void OnTriggerEnter (Collider other){
		if(other.tag == "Player"){
			hideWord.SetActive(false);  //false to hide
		}
	}

   public void OnTriggerExit (Collider other){
            if(other.tag == "Player"){
                hideWord.SetActive(true);  //true to show
            }
        }

}
