using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
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
        direction = new Vector3(0,0,0);
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        /* 
        if(Input.GetAxisRaw("Horizontal") == 0){
            direction += new Vector3(0,0,1);
        }
        */
        /* 
        if(Input.GetKey(KeyCode.S)){
            direction += new Vector3(0,0,-1);
        }
        if(Input.GetKey(KeyCode.D)){
            direction += new Vector3(1,0,0);
        }
        if(Input.GetKey(KeyCode.A)){
            direction += new Vector3(-1,0,0);
        }
        */
    }
    void FixedUpdate(){
        rb.transform.position += direction * speed * Time.deltaTime;
    }
}
