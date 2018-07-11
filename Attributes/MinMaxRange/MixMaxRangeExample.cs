using UnityEngine;
using EditorKit;

public class MinMaxRangeExample : MonoBehaviour 
{
    [MinMaxRange(1.0f, 2.0f)]
    public Vector2 spawnRange;

    [MinMaxRange(1.2f, 2.5f)]
    public Vector2 shootRange = new Vector2(0.0f, 3.0f);  // allowed to start outside the range
}
