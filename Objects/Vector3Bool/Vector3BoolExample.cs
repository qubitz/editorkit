using UnityEngine;
using EditorKit;

public class Vector3BoolExample : MonoBehaviour 
{
    public Vector3Bool unnamedData;
    public Vector3Bool assignedData = new Vector3Bool(true, false, true);
    public Vector3Bool assignedData1 = new Vector3Bool(0xA);  // = 1010
    public Vector3Bool namedData = new Vector3Bool("1", "2", "3");
    public Vector3Bool nameedAssignedData = new Vector3Bool("a", true, "b", false, "c", false);
}
