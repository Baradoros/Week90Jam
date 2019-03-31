using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStation : Station
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckRequirements()
    {
        CollisionCube cCube = CollisionCube.GetComponent<CollisionCube>();

        foreach (GameObject go in cCube.CollidingGameObjects)
            if ((PlayerMask.value & 1 << go.gameObject.layer) == 1 << go.gameObject.layer)
                return true;

        return false;
    }

    public override void DoWorkResult()
    {
        Debug.Log("WORKDONE!");
    }
}
