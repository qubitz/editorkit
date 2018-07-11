using UnityEngine;
using EditorKit;

public class OptionalExample : MonoBehaviour 
{
    // There is two ways to use the `Optional` attribute:

    // #1: ONLY works for object references or fields that can be null.
    //     Sorry, doesn't not work for Nullable types.
    [Optional]
    public GameObject player;
    // or
    [Optional(OptionalAttribute.Format.NewLine)]
    public GameObject player1;

    // #2: Works for ANY field type, but requires a public boolean to
    //     determine whether to show or not.
    [HideInInspector]
    public bool showEnemyCount = false;
    [Optional(nameof(showEnemyCount))]
    public int enemyCount;
    //or
    [HideInInspector]
    public bool showEnemyCount1 = true;
    [Optional("showEnemyCount1", OptionalAttribute.Format.NewLine)]
    public int enemyCount1;
}
