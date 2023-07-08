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

    private Dictionary<Vector2, Tile> _tiles;

    void Awake()
    {
        Instance = this;
    }


    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 1; x < _width + 1; x++)
        {
            for (double y = 0; y < _height * 1.2; y+=1.2)
            {
                var isOffset = x % 2 == 0 && math.abs(y % 2.4 - 1.2) <= 0.00001 || x % 2 == 1 && (y % 2.4 == 0 || y == 0);
                var spawnedTile = Instantiate((isOffset ? _tilePrefab : _tilePrefab2), new Vector3(x, (float)y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                _tiles[new Vector2(x, (float)y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
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
