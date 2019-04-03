using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantControllerDevUse : MonoBehaviour
{
    [Header("Which player is this?")]

    private string horizontalInputString; // use this string 
    private string verticalInputString;
    private string actionInputString;
    private string pickupInputString;
    [Space]
    public GameObject CollisionCube;
    public LayerMask StationMask;
    [Space]
    public float speed;
    [Space]
    private CarryObject carryObject;

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        
        horizontalInputString = "Horizontal2";
        verticalInputString   = "Vertical2";
        actionInputString     = "Action1";
        pickupInputString     = "Pickup1";

        carryObject = GetComponentInChildren<CarryObject>();
        
        if (carryObject == null)
        {
            Debug.LogError("No CarryObject script in children for " + gameObject.name);
        }

    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis(horizontalInputString);
        float moveVertical = -Input.GetAxis(verticalInputString);
        direction = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);

        if (moveHorizontal != 0 || moveVertical != 0)
            rb.transform.rotation = Quaternion.LookRotation(-direction);

        //Do Work On Station
        if (Input.GetButtonDown(actionInputString))
        {
            Station station = null;

            // Get the first object collided with collision cube against StationMask
            foreach (GameObject collision in CollisionCube.GetComponent<CollisionCube>().CollidingGameObjects)
                if ((StationMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
                {
                    station = collision.GetComponent<Station>();
                    break;
                }

            if (station != null && station.CheckRequirements())
                station.DoAction();
        }


        if(Input.GetButtonDown(pickupInputString))
        {
            Debug.Log("Pickup pressed");
            if (carryObject.objectHeld)
            {
            Debug.Log("dropping");
                carryObject.DropUpObject(carryObject.objectHeld);
                carryObject.objectHeld = null;
            }

            if (carryObject.potentialHoldObject != null)
            {
            Debug.Log("pickingup");
                carryObject.PickUpObject(carryObject.potentialHoldObject);
                carryObject.objectHeld = carryObject.potentialHoldObject;
                carryObject.potentialHoldObject = null;
            }
        }
    }
    
    void FixedUpdate() {
        // Always set velocity, never position. 
        // Setting position will move the object while setting velocity will attempt to move it next frame if no collision would occur. 
        // Setting position directly leads to collision bugs.
        rb.velocity = direction;
    }
}