using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public GameObject potentialHoldObject;
    public GameObject objectHeld;
    public Vector3 originalPos;
    public bool isUsingGravity;

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
        originalPos = gObj.transform.position;
        gObj.transform.parent = transform;
        gObj.transform.localPosition = Vector3.zero;
    }
    
    public void DropUpObject(GameObject gObj)
    {
        gObj.transform.parent = null;
        
        if (isUsingGravity) return;
        Vector3 newPos = gObj.transform.position;
        newPos.y = originalPos.y;
        gObj.transform.position = newPos;
    }
}