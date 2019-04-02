using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerAndController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int controllerNumber = 1; controllerNumber <= 4; controllerNumber++)
        {
            if(Input.GetButtonDown("Action" + controllerNumber))
            {
                Debug.Log("Heard controller" + controllerNumber);
                bool controllerAlreadyAssigned = false;
                for(int playerNumber = 1; playerNumber <= 4; playerNumber++)
                {
                    if(StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber) == controllerNumber)
                    {
                        controllerAlreadyAssigned = true;
                        StaticPlayerControllerMapping.SetControllerNumberForPlayer(playerNumber, 0);
                        Debug.Log("Reset player" + playerNumber);
                    }
                }

                if (!controllerAlreadyAssigned)
                {
                    for(int playerNumber = 1; playerNumber <= 4; playerNumber++)
                    {
                        if(StaticPlayerControllerMapping.GetControllerNumberForPlayer(playerNumber) == 0)
                        {
                            StaticPlayerControllerMapping.SetControllerNumberForPlayer(playerNumber, controllerNumber);
                            Debug.Log("Assigning controller" + controllerNumber + "to player" + playerNumber);
                            break;
                        }
                    }
                }
            }
        }
    }
}
