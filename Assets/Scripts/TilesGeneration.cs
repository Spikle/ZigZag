using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGeneration : MonoBehaviour
{
    public static TilesGeneration instance;

    public GameSetting gameSetting;
    private GameLevel gameLevel;

    [Header("Tiles")]
    public string tilePref;
    public List<Tile> tiles;

    public SphereController sphere;


    private Vector3 lastTilePos;
    private int lastPosCrystal = -1;

    public void Awake()
    {
        instance = this;
        gameLevel = gameSetting.GetLevel();
    }

    public void Start()
    {
        tiles = new List<Tile>();
        GenerateStartTilesPlatform();
        GenerateTileWay(gameLevel.tileWayLength, gameLevel.widthWay);

        sphere.speed = gameLevel.speed;
    }

    public void Restart()
    {
        foreach(var tile in tiles)
        {
            tile.ReturnToPool();
        }

        tiles = new List<Tile>();
        GenerateStartTilesPlatform();
        GenerateTileWay(gameLevel.tileWayLength, gameLevel.widthWay);
        sphere.Restart();
    }

    public void Update()
    {
        float dist = (sphere.GetPosition() - lastTilePos).magnitude;
        if (dist < gameLevel.distSpawnWay) GenerateTileWay(1, gameLevel.widthWay);

        DestroyTiles();
    }

    public void DestroyTiles()
    {
        if (tiles.Count == 0) return;

        Tile tile = tiles[0];
        float dist = (sphere.GetPosition() - tile.transform.position).magnitude;
        if (dist > gameLevel.distDestroyWay)
        {
            tile.DestoryTile();
            tiles.RemoveAt(0);
        }

    }

    public void GenerateStartTilesPlatform()
    {
        for(int x = 0; x < gameLevel.platformSize; x++)
            for (int y = 0; y < gameLevel.platformSize; y++)
            {
                GenerateTile(new Vector3(x, 0, y));
            }

        lastTilePos = new Vector3((gameLevel.platformSize -1) - gameLevel.widthWay + 1, 0, (gameLevel.platformSize - 1) - gameLevel.widthWay + 1);
    }

    public void GenerateTileWay(int length, int width)
    {
        for (int i = 0; i < length; i++)
        {
            Vector3 offset = (Random.Range(0, 2) == 0) ? new Vector3(width, 0, 0) : new Vector3(0, 0, width);
            Vector3 position = offset;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < width; y++)
                {
                    position = offset + new Vector3(x,0,y);
                    Tile tile = GenerateTile(lastTilePos + position);
                    GenerateCrystal(tile);
                }

            lastTilePos += offset;
        }
    }

    Vector3[] crystalPosition = new Vector3[] {new Vector3(-0.4f, 0.75f, -0.4f),
        new Vector3(-0.4f, 0.75f, 0.4f),
        new Vector3(0, 0.75f, 0),
        new Vector3(0.4f, 0.75f, -0.4f),
        new Vector3(0.4f, 0.75f, 0.4f)};

    public void GenerateCrystal(Tile tile)
    {
        if (gameLevel.probabilitySpawnCrystal < Random.Range(0, 100)) return;

        if (gameLevel.crystalSpawn == CrystalSpawn.Random)
        {
            int index = Random.Range(0, crystalPosition.Length);
            tile.GenerateCrystal(tile.transform.position + crystalPosition[index]);
        }
        else if (gameLevel.crystalSpawn == CrystalSpawn.InOrder)
        {
            lastPosCrystal++;
            if (lastPosCrystal >= crystalPosition.Length) lastPosCrystal = 0;

            tile.GenerateCrystal(tile.transform.position + crystalPosition[lastPosCrystal]);
        }
    }


    private Tile GenerateTile(Vector3 position)
    {
        GameObject go = PoolManager.instance.GetObject(tilePref, position, Quaternion.identity);
        Tile tile  = go.GetComponent<Tile>();
        tiles.Add(tile);
        return tile;
    }
}
