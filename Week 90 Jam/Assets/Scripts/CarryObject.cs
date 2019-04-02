using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public LayerMask pickupableLayer;
    public GameObject potentialHoldObject;
    public GameObject objectHeld;


    /*
     * 
     *      PICKUP UPDATE SCRIPT MOVED TO MUTANT CONTROLLER SCRIPT FOR PICKUP INPUT BINDING. (RAZ)
     * 
     */

    private void OnTriggerEnter(Collider other)
    {
        potentialHoldObject = other.gameObject;
    }
    
    private void OnTriggerExit(Collider other)
    {
        potentialHoldObject = null;
    }

    public void PickUpObject(GameObject gObj)
    {
        gObj.transform.parent = transform.parent.transform;
        gObj.transform.position += new Vector3(0, 0.61f, 0);
    }
    
    public void DropUpObject(GameObject gObj)
    {
        gObj.transform.parent = null;
        gObj.transform.position -= new Vector3(0, 0.61f, 0);
    }
}