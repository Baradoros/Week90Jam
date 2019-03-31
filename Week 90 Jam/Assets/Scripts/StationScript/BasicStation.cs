using UnityEngine;

public class BasicStation : Station
{
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

        var giveObjective = GetComponent<ObjectiveGiver>();
        if (giveObjective != null)
        {
            giveObjective.GiveObjectiveToPlayer();
        }
    }
}