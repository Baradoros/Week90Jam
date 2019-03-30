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
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(0,0,0);
        direction = new Vector3(moveHorizontal, 0, moveVertical);
        //LOOK
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.transform.rotation = Quaternion.LookRotation(-movement);
    }
    void FixedUpdate(){
        rb.transform.position += direction * speed * Time.deltaTime;
    }
}
