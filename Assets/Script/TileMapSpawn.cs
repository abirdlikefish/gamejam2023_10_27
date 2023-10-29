using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSpawn : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tiles;
    public int mapSize = 10;
    public Transform player;

    Vector3Int lastLoadedPos;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        lastLoadedPos = GetTilemapPosition(player.position);
        LoadTiles(lastLoadedPos);
    }

    void Update()
    {
        Vector3Int currentPos = GetTilemapPosition(player.position);
        if (currentPos != lastLoadedPos)
        {
            LoadTiles(currentPos);
            UnloadTiles(currentPos);
            lastLoadedPos = currentPos;
        }
    }

    Vector3Int GetTilemapPosition(Vector3 position)
    {
        return tilemap.WorldToCell(position);
    }

    public void LoadTiles(Vector3Int position)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = position.x - mapSize; x < position.x + mapSize; x++)
        {
            for (int y = position.y - mapSize; y < position.y + mapSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (tilemap.GetTile(tilePos) == null)
                {
                    tilemap.SetTile(tilePos, tiles[Random.Range(0, tiles.Length)]);
                }
            }
        }
    }

    public void UnloadTiles(Vector3Int position)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = position.x - mapSize - 1; x <= position.x + mapSize + 1; x++)
        {
            for (int y = position.y - mapSize - 1; y <= position.y + mapSize + 1; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (Mathf.Abs(position.x - x) > mapSize || Mathf.Abs(position.y - y) > mapSize)
                {
                    tilemap.SetTile(tilePos, null);
                }
            }
        }
    }
}
