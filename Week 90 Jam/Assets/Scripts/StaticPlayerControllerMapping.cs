using System.Collections;
using System.Collections.Generic;

public static class StaticPlayerControllerMapping
{
    private static int[] playerControllerMapping = new int[4] {0, 0, 0, 0}; // 4 is the max number of players that would play the game.

    // player number must between 1 to max number of players (4)
    public static int GetControllerNumberForPlayer(int playerNumber)
    {
        if(playerNumber > 0 && playerNumber <= 4)
        {
            return playerControllerMapping[playerNumber - 1]; // playerNumber - 1 for converting to array index.
        }

        return 0; //invalid controller number.
    }

    // sets the player controller number for a player
    public static bool SetControllerNumberForPlayer(int playerNumber, int controllerNumber)
    {
        if(playerNumber > 0 && playerNumber <= 4)
        {
            playerControllerMapping[playerNumber - 1] = controllerNumber; // playerNumber - 1 for converting to array index.
            return true;
        }

        return false;
    }
}
