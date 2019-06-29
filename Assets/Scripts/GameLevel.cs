using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameLevel", menuName = "Game Level")]
public class GameLevel : ScriptableObject
{
    [Header("Tiles")]
    public int platformSize = 3;
    public int tileWayLength = 10;
    public float distSpawnWay = 10;
    public float distDestroyWay = 2;
    public int widthWay = 1;

    [Header("Sphere")]
    public int speed = 5;

    [Header("Crystal")]
    [Range(0, 100)]
    public float probabilitySpawnCrystal = 50f;
    public CrystalSpawn crystalSpawn = CrystalSpawn.Random;
}
