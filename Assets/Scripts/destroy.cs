using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
     public GameObject hideObject;

    void OnDestroy()
    {
        if(gameObject != null){
        hideObject.SetActive(false);
    }
    }
}
