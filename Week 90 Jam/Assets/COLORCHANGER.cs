using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLORCHANGER : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        StartCoroutine(colorchange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator colorchange(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            int randomint = Random.Range(0, 3);
            if(randomint == 0){
                cam.backgroundColor = Color.red;
            } 
            else if(randomint == 1){
                cam.backgroundColor = Color.blue;
            }
            else if(randomint == 2){
                cam.backgroundColor = Color.white;
            }
            else if(randomint == 3){
                cam.backgroundColor = Color.green;
            }
        }
    }
}
