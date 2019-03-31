using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [Header("Mission Info")]
    public string missionTitle;
    public TextMeshProUGUI missionTitleText;
    public bool isComplete;
    
    [Header("Objectives Info")]
    public GameObject objectivePrefab;
    public Transform objCardsPanel;
    public List<ObjectiveData> Objectives = new List<ObjectiveData>();


    private void Start()
    {
        missionTitleText.text = missionTitle;
        BuildMissionCard();
    }

    private void BuildMissionCard()
    {
        foreach (ObjectiveData objectiveData in Objectives)
        {
            var oCard = Instantiate(objectivePrefab, transform.position, transform.rotation, objCardsPanel);
            oCard.name = objectiveData.name + " added"; // debug
            oCard.GetComponent<Image>().sprite = objectiveData.objectiveSprite;

        }
    }

    // iterate through the players objectives, see if the player is holding a matching this mission
    // if they are holding one, it is queued for deletion from their holdings
    // each time a match is found, the count is decreased, if at the end, the mission countdown is at 0,
    // then all of this missions objectives are removed from player and returns TRUE 
    public bool CompareObjectives_MissionToPlayer()
    {
        // clone lists so I don't effect them
        var pl = FindObjectOfType<ObjectivesHeld>().Objectives.ToList();        
        var ml = Objectives.ToList();        
        List<ObjectiveData> ToRemoveFromPlayer = new List<ObjectiveData>();        
        var c = Objectives.Count;

        // dont bother running if the players holdings is less than the mission objective count
        if (pl.Count >= ml.Count)
        {
            foreach (ObjectiveData objectiveData in pl.ToArray())
            {
                foreach (ObjectiveData data in ml.ToArray())
                {
                    if (data == objectiveData)
                    {
                        ToRemoveFromPlayer.Add(data);
                        ml.Remove(data);
                        pl.Remove(data);
                        c--;
                        break;
                    }
                }
            }   
        }

        // return values based in the criteria noted above
        if (c == 0)
        {
            // Mission Complete
            // Debug.Log("true, not marked completed in Debug Mode");
            isComplete = true;
            gameObject.SetActive(false);
            
            // Debug.Log("In Debug, not removed right now.");
            // Remove them from the player holding
            foreach (ObjectiveData objectiveData in ToRemoveFromPlayer)
            {
                FindObjectOfType<ObjectivesHeld>().Objectives.Remove(objectiveData);
            }
            
            FindObjectOfType<ObjectivesHeld>().UpdateObjectivesVisual();
            return true;
        }
        else
        {
            // Debug.Log("false");
            return false;
        }
    }
}

[CustomEditor(typeof(Mission))]
public class MissionHelperEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();
        
        Mission m = (Mission) target;
        
        if (GUILayout.Button("Compare w/ Player"))
        {
            m.CompareObjectives_MissionToPlayer();
        }
    }
}