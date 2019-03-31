using System.Collections;
using UnityEngine;

public class MissionCheckStation : Station
{
    public bool isUsable = true;
    
    public override bool CheckRequirements()
    {
        if (!isUsable) return false;
        
        CollisionCube cCube = collisionCube.GetComponent<CollisionCube>();

        foreach (GameObject go in cCube.CollidingGameObjects)
            if ((playerMask.value & 1 << go.gameObject.layer) == 1 << go.gameObject.layer)
                return true;

        return false;
    }

    public override void DoWorkResult()
    {
        Debug.Log("Just Check! No Wait");
        
        // don't want repeated usages, disabling for 2 seconds then re-enabling
        isUsable = false;
        StartCoroutine(ResetUsable());
        
        // execute a compare
        CheckMissions();
    }

    IEnumerator ResetUsable()
    {
        yield return new WaitForSecondsRealtime(2f);
        isUsable = true;
    }

    public void CheckMissions()
    {
        Mission[] missions = FindObjectsOfType<Mission>();

        if (missions == null) return;

        foreach (Mission mission in missions)
        {
            mission.CompareObjectives_MissionToPlayer();
        }
    }
}