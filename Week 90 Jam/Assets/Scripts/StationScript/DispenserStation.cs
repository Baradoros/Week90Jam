using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserStation : Station
{
    [Header("Dispenser Variables")]
    public GameObject DispensingObject;
    [Header("Position of Dispensed Object Relative to Dispenser")]
    public Vector3 DispenseDisplacement;

    public override bool CheckRequirements()
    {
        CollisionCube cCube = collisionCube.GetComponent<CollisionCube>();

        foreach (GameObject go in cCube.CollidingGameObjects)
            if ((playerMask.value & 1 << go.gameObject.layer) == 1 << go.gameObject.layer)
                return true;

        return false;
    }

    public override void DoWorkResult()
    {
        Instantiate(DispensingObject, transform.position + DispenseDisplacement, Quaternion.identity);
    }
}
