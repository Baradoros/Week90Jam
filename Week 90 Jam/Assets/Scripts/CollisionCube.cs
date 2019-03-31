using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCube : MonoBehaviour
{
    [Header("What Masks To Look For?")]
    public List<LayerMask> LayerMasks;
    [HideInInspector]
    public List<int> MaskIDs;
    [HideInInspector]
    public List<GameObject> CollidingGameObjects;

    void Start()
    {
        CollidingGameObjects = new List<GameObject>();

        MaskIDs = new List<int>();
        //JUST REALIZED HOW MASK VALUES WORK!!! I will redo this and make it better. I am ashamed.
        // Bardoros: https://giphy.com/gifs/pDsCoECKh1Pa
        foreach (LayerMask mask in LayerMasks)
            MaskIDs.Add((int) Mathf.Log(mask.value, 2));
    }

    void OnTriggerEnter(Collider coll)
    {
        if (MaskIDs.Contains(coll.gameObject.layer))
            CollidingGameObjects.Add(coll.gameObject);
    }

    void OnTriggerExit(Collider coll)
    {
        CollidingGameObjects.Remove(coll.gameObject);
    }

}
