using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStation : Station
{
    [Header("Input Variables")]
    public List<GameObject> AcceptedItems;
    public GameObject AcceptingItem; //Item currently being accepted

    [HideInInspector]
    public List<string> AcceptedItemNames = new List<string>();

    private void Awake()
    {
        foreach (GameObject go in AcceptedItems)
            AcceptedItemNames.Add(go.GetComponent<PickUpIdentifier>().ID);
    }

    public override bool CheckRequirements()
    {
        CollisionCube cCube = collisionCube.GetComponent<CollisionCube>();

        foreach (GameObject go in cCube.CollidingGameObjects)
            if ((playerMask.value & 1 << go.gameObject.layer) == 1 << go.gameObject.layer)
            {
                AcceptingItem = go.transform.GetComponentInChildren<CarryObject>().objectHeld;

                if(AcceptingItem != null && AcceptedItemNames.Contains(AcceptingItem.GetComponent<PickUpIdentifier>().ID))
                    return true;
            }

        return false;
    }

    public override void DoWorkResult()
    {
        Debug.Log("ItemAccepted");

        if(AcceptingItem != null)
            Destroy(AcceptingItem);
    }
}
