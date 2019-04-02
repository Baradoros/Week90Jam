using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    [Header("Which player is this?")]
    [SerializeField]
    private int playerNumber = 1;
    // controllerNumber indicates the controller the player is using. 0 indicates no player has possessed this Character.
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


    private Rigidbody rb;
    private Vector3 direction;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        if(playerNumber > 0 && playerNumber <= 4)
        {
            controllerNumber = StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber);
        }

        if(controllerNumber == 0)
        {
            gameObject.SetActive(false); // hides the player if no player has possessed this player.
        } else 
        {
            horizontalInputString = "Horizontal" + controllerNumber;
            verticalInputString   = "Vertical" + controllerNumber;
            actionInputString     = "Action" + controllerNumber;
            pickupInputString     = "Pickup" + controllerNumber;
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
        if (Input.GetAxisRaw(actionInputString) > 0)
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
                station.DoWork();
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
