using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public int roomID;
    public int numberOfPlayers;
    public bool publicRoom;
    public bool teamGame;
    public int mapID;

    public GameSettings()
    {
        numberOfPlayers = 2;
        publicRoom = true;
        teamGame = true;
        mapID = 0;

    }

    public string Parse()
    {
        return "";
    }

}
