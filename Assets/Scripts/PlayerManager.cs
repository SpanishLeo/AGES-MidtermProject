using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public int PlayerNumber { get; private set; }
    public bool IsJoined { get; set; }

    public PlayerManager(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }
}
