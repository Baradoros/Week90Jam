﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int isotopes;
    public GameObject isotopeobj;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HARD CODE ON PURPOSE! RAZ!
        if(Input.GetKeyDown(KeyCode.G)){
            Instantiate(isotopeobj, this.transform.position + offset, this.transform.rotation);
        }
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "isotope"){
            isotopes += 1;
            Destroy(col.gameObject);
        }
    }
}