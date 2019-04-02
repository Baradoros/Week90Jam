using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSelectionTextChanger : MonoBehaviour
{
    public int playerNumber; // The player associated with PlayerSelection Text
    public string notSelectedText = "Press 'Action' button to activate";
    public string selectedText1 = "Controller ";
    public string selectedText2 = " is Player ";


    private Text actionText;

    // Start is called before the first frame update
    void Start()
    {
        actionText = gameObject.GetComponent<Text>();
        if(actionText == null)
        {
            Debug.LogError("No Text component found in " + gameObject);
        }
    
        if(playerNumber < 0 && playerNumber > 4) // checking for invalid playernumber
        {
            Debug.LogError("Invalid player number in " + gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNumber > 0 && playerNumber <= 4 && StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber) != 0)
        {
            actionText.text = selectedText1 + StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber) + selectedText2 + playerNumber;
        } else
        {
            actionText.text = notSelectedText;
        }
    }
}
