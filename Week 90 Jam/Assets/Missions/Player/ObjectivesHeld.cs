using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesHeld : MonoBehaviour
{
    [Header("Objectives Info")]
    public GameObject objectivePrefab;
    public Transform objCardsPanel;
    public List<ObjectiveData> Objectives = new List<ObjectiveData>();
    
    private void Start()
    {
        UpdateObjectivesVisual();
    }

    public void AddObjective(ObjectiveData oD)
    {
        Objectives.Add(oD);
        UpdateObjectivesVisual();
    }
    
    private void BuildObjectivesHeld()
    {
        foreach (ObjectiveData objectiveData in Objectives)
        {
            var oCard = Instantiate(objectivePrefab, transform.position, transform.rotation, objCardsPanel);
            oCard.name = objectiveData.name + " added"; // debug
            oCard.GetComponent<Image>().sprite = objectiveData.objectiveSprite;
        }
    }

    private void ClearObjectivesHeldVisual()
    {
        ObjCard[] objCards = objCardsPanel.GetComponentsInChildren<ObjCard>();
        foreach (ObjCard v in objCards)
        {
            Destroy(v.gameObject);
        }
    }

    public void UpdateObjectivesVisual()
    {
        if (!objCardsPanel) return;
        
        ClearObjectivesHeldVisual();
        BuildObjectivesHeld();
    }
}

[CustomEditor(typeof(ObjectivesHeld))]
public class HeldHelperEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();
        
        ObjectivesHeld t = (ObjectivesHeld) target;
         
        if (GUILayout.Button("Update Held Visual"))
        {
            t.UpdateObjectivesVisual();
        }
    }
}