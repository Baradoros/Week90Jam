﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    [Header("Which player is this?")]
    [SerializeField]
    private int playerNumber = 1;
    // controllerNumber indicates the controller the player is using. 0 indicates no player has possessed this Character.
    [SerializeField]
    private int controllerNumber = 0;
    [Space]
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

    private void Start() {
        rb = GetComponent<Rigidbody>();
        if(controllerNumber == 0 && playerNumber > 0 && playerNumber <= 4)
        {
            controllerNumber = StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber);
        }

        if(controllerNumber == 0) // Still controller not set? then disable the game object as no player possess it
        {
            gameObject.SetActive(false); // hides the player if no player has possessed this player.
        } else 
        {
            horizontalInputString = "Horizontal" + controllerNumber;
            verticalInputString   = "Vertical" + controllerNumber;
            actionInputString     = "Action" + controllerNumber;
            pickupInputString     = "Pickup" + controllerNumber;
        }

        carryObject = GetComponentInChildren<CarryObject>();
        if (carryObject == null)
        {
            Debug.LogError("No CarryObject script in childer for " + gameObject);
        }

    }

    void Update()
    {
        if (controllerNumber == 0)
        {
            return; 
        }

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

    void SetControllerNumber(int controllerNumberParam)
    {
        controllerNumber = controllerNumberParam;
        if (controllerNumber != 0)
        {
            gameObject.SetActive(true);
            horizontalInputString = "Horizontal" + controllerNumber;
            verticalInputString = "Vertical" + controllerNumber;
            actionInputString = "Action" + controllerNumber;
            pickupInputString = "Pickup" + controllerNumber;
        }
    }
}
