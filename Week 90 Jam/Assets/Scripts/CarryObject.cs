using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public LayerMask pickupableLayer;
    public GameObject potentialHoldObject;
    public GameObject objectHeld;

    private void Update()
    {        
        if (Input.GetKeyUp(KeyCode.B))
        {
            if (objectHeld)
            {
                DropUpObject(objectHeld);
                objectHeld = null;
            }
            
            if (potentialHoldObject != null)
            {
                PickUpObject(potentialHoldObject);
                objectHeld = potentialHoldObject;
                potentialHoldObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        potentialHoldObject = other.gameObject;
    }
    
    private void OnTriggerExit(Collider other)
    {
        potentialHoldObject = null;
    }

    private void PickUpObject(GameObject gObj)
    {
        gObj.transform.parent = transform.parent.transform;
    }
    
    private void DropUpObject(GameObject gObj)
    {
        gObj.transform.parent = null;
    }
}