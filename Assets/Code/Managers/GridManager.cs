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
    [SerializeField] private Tile SpawnTile;

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
                Tile spawnedTile = Instantiate((isOffset ? _tilePrefab : _tilePrefab2), location, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(canvas.transform,false);

                _tiles[new Vector2(x, (float)y)] = spawnedTile;
            }
        }
        for (double y = 0; y < _height; y++)
        {

            Vector3 location = new Vector3(-350, (float)(y * 60) - 80);
            Tile spawnedTile = Instantiate(SpawnTile, location, Quaternion.identity);
            spawnedTile.name = $"Enemy Tile {-350} {y}";
            spawnedTile.isSelectable = false;
            spawnedTile.transform.SetParent(canvas.transform, false);

            _tiles[new Vector2(-350, (float)y)] = spawnedTile;
        }
    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Value.isSelectable == false).OrderBy(t => Random.value).First().Value;
    }


    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}
