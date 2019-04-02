using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextLevelButtonEnable : MonoBehaviour
{

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No button in " + gameObject);
        }
        else
        {
            button.interactable = false;
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        for(int playerNumber = 1; playerNumber <= 4; playerNumber++)
        {
            if(StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber) != 0)
            {
                button.interactable = true;
                return;
            }
        }
        button.interactable = false;
    }
}
