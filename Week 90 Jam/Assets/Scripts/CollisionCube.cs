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

    // Start is called before the first frame update
    void Start()
    {
        CollidingGameObjects = new List<GameObject>();

        MaskIDs = new List<int>();
        foreach (LayerMask mask in LayerMasks)
            MaskIDs.Add((int) Mathf.Log(mask.value, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
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
