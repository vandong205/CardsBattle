using Unity.VisualScripting;
using UnityEngine;

public class CardController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSprite(int rank,int suitID)
    {
      
        string frontPath = $"card-{(SuidId)suitID}-{rank}";
        Debug.Log(frontPath);
        string backPath = "card-back1";
        Transform frontchild = transform.Find("Front");
        GameObject frontSide = frontchild != null ? frontchild.gameObject : null;
        Transform backchild = transform.Find("Back");
        GameObject backSide = backchild != null ? backchild.gameObject : null;
        if (frontSide != null) {
            frontSide.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"PlayingCards/{frontPath}");
        }
        if (backSide != null)
        {
            backSide.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"PlayingCardsBack/{backPath}");
        }
    }
}
