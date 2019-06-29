using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalSpawn { Random, InOrder }
[CreateAssetMenu(fileName = "GameSetting", menuName = "Game Setting")]
public class GameSetting : ScriptableObject
{
    public int currentLevel = 0;
    public GameLevel[] gameLevels;

    public GameLevel GetLevel()
    {
        return gameLevels[currentLevel];
    }
}
