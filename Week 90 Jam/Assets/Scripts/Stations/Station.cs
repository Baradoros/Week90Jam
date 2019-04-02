using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Station : MonoBehaviour
{
    [Header("Prefab Variables")]
    public GameObject collisionCube;
    public LayerMask playerMask;

    [Header("Work Variables")]
    public float rechargeTime;
    public float workTime;

    [Space]
    public float progress; //Out of 1
    public bool isFinished = false;

    [HideInInspector]
    public bool hasWorkedThisFrame = false;

    void FixedUpdate()
    {
        Recharge();

        if (hasWorkedThisFrame)
            hasWorkedThisFrame = false;
        else
            DoIdleWork();
    }

    //Used purely to have hasWorkedThisFrame set to true
    public void DoAction()
    {
        hasWorkedThisFrame = true;
        DoWork();
    }

    public virtual void DoWork()
    {
        if (!isFinished)
        {
            if (workTime == 0)
                progress = 1;
            else
                progress += (Time.deltaTime / workTime);

            Debug.Log(progress);

            if (progress >= 1)
            {
                isFinished = true;
                progress = 1;

                DoWorkResult();
            }
        }
        else
            Debug.Log("Recharging");
    }

    public virtual void Recharge()
    {
        if(isFinished)
        {
            if (rechargeTime == 0)
                progress = 0;
            else
                progress -= (Time.deltaTime / rechargeTime);

            if(progress <= 0)
            {
                isFinished = false;
                progress = 0;
            }
        }
    }
    
    public virtual void JustCheck()
    {
        isFinished = true;
        DoWorkResult();
        Debug.Log("Recharging");
    }

    //Runs when player is not working on it
    public virtual void DoIdleWork() { }

    public abstract bool CheckRequirements();
    public abstract void DoWorkResult();

}
