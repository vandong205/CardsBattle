using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Desk cardDesk = new Desk();
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
    }
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitGame()
    {
        CardsLoader cardloader = new CardsLoader();
        cardDesk.cardList = cardloader.LoadCardsFromXml();
    }
}
