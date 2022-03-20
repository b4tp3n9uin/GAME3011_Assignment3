using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    public Row[] rows;

    public Tile[,] Tiles { get; private set; }

    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);

    public Item[] items;

    private Tile _selectedTile1;
    private Tile _secectedTile2;

    private readonly List<Tile> _selection = new List<Tile>();

    private const float tweenDuration = 0.25f;

    private void Awake() => Instance = this;

    private void Start()
    {
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {

                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;

                //tile.item = ItemDB.Items[Random.Range(0, ItemDB.Items.Length)];

                tile.Item = items[Random.Range(0, items.Length)];

                Tiles[x, y] = tile;

                
            }
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;

        foreach (var conncectedTile in Tiles[0, 0].GetConnectedTiles())
        {
            conncectedTile.icon.transform.DOScale(1.25f, tweenDuration).Play();
        }
    }
    

    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile))
            _selection.Add(tile);

        if (_selection.Count < 2)
            return;

        Debug.Log($"Selected Tiles: ({_selection[0].x}, {_selection[0].y}) & ({_selection[1].x}, {_selection[1].y})");

        await Swap(_selection[0], _selection[1]);

        _selection.Clear();
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();

        sequence.Join(icon1Transform.DOMove(icon2Transform.position, tweenDuration)).
            Join(icon2Transform.DOMove(icon1Transform.position, tweenDuration));

        await sequence.Play().AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;
    }

    private bool CanPop()
    {
        throw new System.NotImplementedException();
    }

    private void Pop()
    {
        throw new System.NotImplementedException();
    }
}
