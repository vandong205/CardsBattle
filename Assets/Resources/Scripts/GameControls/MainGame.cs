using UnityEngine;
public class MainGame :MonoBehaviour
{
    public static MainGame Instance;
    public GameObject DynamicObject;
    private void Awake()
    {
        DynamicObject = new GameObject("DynamicObject");
        DynamicObject.transform.SetParent(transform);
        if (Instance == null) { 
            Instance = this;
        }
        
    InitGame();
    }
    void InitGame()
    {
        DynamicObject.AddComponent<DynamicObjectManager>(); 
        StreamingResources.Instance.deskObj.transform.SetParent(DynamicObject.transform,false);    }
}