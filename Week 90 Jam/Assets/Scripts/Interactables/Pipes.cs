using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : Station
{
    public GameObject wrench;
    public GameObject reactor;

    public override bool CheckRequirements()
    {
        CollisionCube cCube = collisionCube.GetComponent<CollisionCube>();

        foreach (GameObject go in cCube.CollidingGameObjects)
            if ((playerMask.value & 1 << go.gameObject.layer) == 1 << go.gameObject.layer)
            {
                if (go.transform.GetComponentInChildren<CarryObject>().objectHeld == wrench)
                    return true;
            }

        return false;
    }

    public override void DoWorkResult()
    {
        reactor.GetComponent<ReactorPressure>().pressure += 10;
        reactor.GetComponent<ReactorPressure>().isDecaying = false;
        Debug.Log("Bang bang");
    }
}
