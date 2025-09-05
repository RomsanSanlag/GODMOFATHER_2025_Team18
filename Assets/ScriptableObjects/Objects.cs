using UnityEngine;


[CreateAssetMenu(fileName = "Objects", menuName = "Objects")]
public class Objects : ScriptableObject
{
    public string itemName;
    public int value;
    public GameObject prefab;
    public int index;
}
