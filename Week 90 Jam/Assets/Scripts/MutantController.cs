using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    [Header("Which player is this?")]
    [SerializeField]
    private int playerNumber = 1;
    [Space]
    public GameObject CollisionCube;
    public LayerMask StationMask;
    [Space]
    public float speed;


    private Rigidbody rb;
    private Vector3 direction;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal" + playerNumber);
        float moveVertical = -Input.GetAxis("Vertical" + playerNumber);
        direction = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);

        if (moveHorizontal != 0 || moveVertical != 0)
            rb.transform.rotation = Quaternion.LookRotation(-direction);

        //Do Work On Station
        if (Input.GetAxisRaw("Action" + playerNumber) > 0)
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
    }
    

    void FixedUpdate() {
        // Always set velocity, never position. 
        // Setting position will move the object while setting velocity will attempt to move it next frame if no collision would occur. 
        // Setting position directly leads to collision bugs.
        rb.velocity = direction;
    }
}
