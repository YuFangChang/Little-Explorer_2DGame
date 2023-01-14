using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;  //off set is private cuz we can set that value here in the script
    // Start is called before the first frame update
    void Start()
    {
     offset = transform.position - player.transform.position;  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // as we move our player w/ the controls on the keyboard 
        //that each frame before displaying what the camera can see,
        //the camera is moved into a new position aligned w/ the Player object
        transform.position = player.transform.position + offset;
    }
}