using UnityEngine;
[CreateAssetMenu(menuName = "Data/Object Spawn Config")]
public class ConfigObject : ScriptableObject
{
    public string objname;
    public Vector3 startpostion;
    public Quaternion startrotation;
    public Vector3 startscale;
}