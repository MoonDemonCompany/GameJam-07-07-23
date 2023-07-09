using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{

    public static GridManager Instance;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _tilePrefab2;

    [SerializeField] private Transform _cam;

    [SerializeField] private Canvas canvas;

    private Dictionary<Vector2, Tile> _tiles;

    void Awake()
    {
        Instance = this;
    }


    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (double y = 0; y < _height; y++)
            {
                var isOffset = x % 2 == 0 && math.abs(y % 2 - 1) <= 0.00001 || x % 2 == 1 && (y % 2 == 0 || y == 0);

                Vector3 location = new Vector3((x * 80) -270, (float)(y * 60) - 80);
                var spawnedTile = Instantiate((isOffset ? _tilePrefab : _tilePrefab2), location, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(canvas.transform,false);

                _tiles[new Vector2(x, (float)y)] = spawnedTile;
            }
        }

    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }


    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}
