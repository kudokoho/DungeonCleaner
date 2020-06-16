using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInfo 
{
    public enum State
    {
        None,
        GamePlay,
        GameOver,
        GameClear
    }

    public static State DungeonState = State.None;
}
