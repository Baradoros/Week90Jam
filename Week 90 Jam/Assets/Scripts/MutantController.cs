using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    [Header("Which player is this?")]
    [SerializeField]
    private int playerNumber = 1;

    [Header("Work Variables")]
    public GameObject CollisionCube;
    public LayerMask StationMask;
    [Space]

    public Rigidbody rb;
    public Vector3 direction;
    public float speed;
    public int velz;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal" + playerNumber);
        float moveVertical = -Input.GetAxisRaw("Vertical" + playerNumber);
        direction = new Vector3(0,0,0);
        direction = new Vector3(moveHorizontal, 0, moveVertical);
        //LOOK
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        if (moveHorizontal != 0 || moveVertical != 0)
            rb.transform.rotation = Quaternion.LookRotation(-movement);
        if (Input.GetButtonDown("UP"))
            Debug.Log("LOG");

        //Do Work On Station
        if (Input.GetAxisRaw("Action" + playerNumber) > 0)
        {
            Station station = null;

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
    

    void FixedUpdate(){
        rb.transform.position += direction * speed * Time.deltaTime;
    }
}
