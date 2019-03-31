using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int Red;
    public int Green;
    public int Blue;
    public GameObject isotopeObj;
    public Vector3 offset;
    public GameObject placementObj;
    public LayerMask mask;

    void Start()
    {
        
    }

    void Update()
    {
        //HARD CODE ON PURPOSE! RAZ!
        if(Input.GetKeyDown(KeyCode.G)) {
            Instantiate(isotopeObj, placementObj.transform.position, placementObj.transform.rotation);
        }
    }

    void OnCollisionEnter(Collision col){
        if((mask.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer){
        if(col.gameObject.tag == "Red"){
            Red += 1;
            Destroy(col.gameObject);
        }
        else if(col.gameObject.tag == "Blue"){
            Blue += 1;
            Destroy(col.gameObject);
        }
        else if(col.gameObject.tag == "Green"){
            Green += 1;
            Destroy(col.gameObject);
        }
        }
    }
}
