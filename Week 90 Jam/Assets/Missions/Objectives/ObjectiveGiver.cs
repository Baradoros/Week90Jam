using UnityEngine;

public class ObjectiveGiver : MonoBehaviour
{
    public ObjectiveData objectiveToGive;

    public void GiveObjectiveToPlayer()
    {
        var player = FindObjectOfType<MutantController>();
        if (player != null)
        {
            player.GetComponent<ObjectivesHeld>().AddObjective(objectiveToGive);
        }
    }
}
