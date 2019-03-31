using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Station : MonoBehaviour
{
    [Header("Prefab Variables")]
    public GameObject CollisionCube;
    public LayerMask PlayerMask;

    [Header("Work Variables")]
    public float RechargeTime;
    public float WorkTime;

    [Space]
    public float Progress; //Out of 1
    public bool IsFinished = false;

    [HideInInspector]
    public bool HasWorkedThisFrame = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Recharge();

        if (HasWorkedThisFrame)
            HasWorkedThisFrame = false;
        else
            DoIdleWork();
    }

    public virtual void DoWork()
    {
        if (!IsFinished)
        {
            if (WorkTime == 0)
                Progress = 1;
            else
                Progress += (Time.deltaTime / WorkTime);

            Debug.Log(Progress);

            if (Progress >= 1)
            {
                IsFinished = true;
                Progress = 1;

                DoWorkResult();
            }
        }
        else
            Debug.Log("Recharging");
    }

    public virtual void Recharge()
    {
        if(IsFinished)
        {
            if (RechargeTime == 0)
                Progress = 0;
            else
                Progress -= (Time.deltaTime / RechargeTime);

            if(Progress <= 0)
            {
                IsFinished = false;
                Progress = 0;
            }
        }
    }
    
    public virtual void JustCheck()
    {
        IsFinished = true;
        DoWorkResult();
        Debug.Log("Recharging");
    }

    //Runs when player is not working on it
    public virtual void DoIdleWork() { }

    public abstract bool CheckRequirements();
    public abstract void DoWorkResult();

}
