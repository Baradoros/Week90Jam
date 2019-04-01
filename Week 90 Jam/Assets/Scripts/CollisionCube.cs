using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCube : MonoBehaviour
{
    [Header("What Masks To Look For?")]
    public LayerMask Layers;
    [HideInInspector]
    public List<GameObject> CollidingGameObjects;

    void Start()
    {
        CollidingGameObjects = new List<GameObject>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if ((Layers.value & 1 << coll.gameObject.layer) == 1 << coll.gameObject.layer)
            CollidingGameObjects.Add(coll.gameObject);
    }

    void OnTriggerExit(Collider coll)
    {
        CollidingGameObjects.Remove(coll.gameObject);
    }

}
