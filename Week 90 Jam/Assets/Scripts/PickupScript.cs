using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int isotopes;
    public GameObject isotopeobj;
    public Vector3 offset;
    public GameObject placementobj;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HARD CODE ON PURPOSE! RAZ!
        if(Input.GetKeyDown(KeyCode.G)){
            Instantiate(isotopeobj, placementobj.transform.position, placementobj.transform.rotation);
        }
    }
    void OnCollisionEnter(Collision col){
        if((mask.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer){
        if(col.gameObject.tag == "isotope"){
            isotopes += 1;
            Destroy(col.gameObject);
        }
        }
    }
}
