using UnityEngine;

public class BasicStation : Station
{
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
        Debug.Log("WORKDONE!");

        var giveObjective = GetComponent<ObjectiveGiver>();
        //if (giveObjective != null)
        //{
        //    giveObjective.GiveObjectiveToPlayer();
        //}
        //
        // Condensed all of this to the below line
        giveObjective?.GiveObjectiveToPlayer();
    }
}