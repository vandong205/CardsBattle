using UnityEngine;

public class StreamingResources : MonoBehaviour
{
    public static StreamingResources Instance;
    public Desk cardDesk;
    public GameObject deskObj;
    public Card showingCard;
    void Awake()
    {
        // Giữ duy nhất 1 instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ khi load scene mới
        }
        else
        {
            Destroy(gameObject);
        }
        SetUpResources();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpResources()
    {
        cardDesk = new Desk();
        showingCard = new Card(0, 0);

        deskObj = new GameObject("Desk");

        //ConfigObject deskSetUp = Resources.Load<ConfigObject>("ScriptedObjects/Desk");
        //deskObj.transform.position = deskSetUp.startpostion;
        //deskObj.transform.rotation = deskSetUp.startrotation;
        //deskObj.transform.localScale = deskSetUp.startscale;

        CardsLoader cardloader = new CardsLoader();
        cardDesk.cardList = cardloader.LoadCardsFromXml();

        GameObject deskPrefab = Resources.Load<GameObject>("Prefabs/Desk");
        GameObject deskPrefabInstance = Instantiate(deskPrefab);
        deskPrefabInstance.transform.SetParent(deskObj.transform, false);

        Animator deskAnimator = deskObj.GetComponent<Animator>();
        if (deskAnimator == null)
            deskAnimator = deskObj.AddComponent<Animator>();
        RuntimeAnimatorController deskcontroller = Resources.Load<RuntimeAnimatorController>("Animations/DeskAnimationCtr");
        deskAnimator.runtimeAnimatorController = deskcontroller;
    }
}
