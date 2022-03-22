using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scirptable Object Class for the Tile objects called 'Items' 
[CreateAssetMenu(menuName = "Match3/Items")]
public class Item : ScriptableObject
{
    public Sprite sprite;

    public int value;
}
