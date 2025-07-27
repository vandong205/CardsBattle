using UnityEngine;
using System.Collections;

public class DeskController : MonoBehaviour, IClickable
{
    [SerializeField] public int numberOfCards = 52;
    [SerializeField] public float offsety = 0.1f;
    [SerializeField] public float offsetx = 0.02f;
    [SerializeField] public GameObject CardsbackPrefabs;

    private GameObject cardA;
    private GameObject cardB;
    private bool useCardA = true;
    private Vector3 showCardPosition = new Vector3(8.62f, 3.45f, 0); // Vị trí hiển thị

    void Start()
    {
        CreateVisualDesk();
        InitCardObjects();
    }

    private void CreateVisualDesk()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 offset = new Vector3(i * offsetx, i * offsety, -i * 0.001f);
            Instantiate(CardsbackPrefabs, transform.position + offset, Quaternion.identity, transform);
        }
    }

    private void InitCardObjects()
    {
        GameObject cardprefab = Resources.Load<GameObject>("Prefabs/Card");

        cardA = Instantiate(cardprefab, transform.position, Quaternion.identity, MainGame.Instance.DynamicObject.transform);
        cardB = Instantiate(cardprefab, transform.position, Quaternion.identity, MainGame.Instance.DynamicObject.transform);

        cardA.name = "CardA";
        cardB.name = "CardB";

        cardA.SetActive(false);
        cardB.SetActive(false);

        SetSortingOrder(cardA, 1);
        SetSortingOrder(cardB, 2);
    }

    public void RemoveTopCard()
    {
        if (transform.childCount == 0) return;
        Transform topCard = transform.GetChild(transform.childCount - 1);
        int cardRemain = StreamingResources.Instance.cardDesk.cardList.Count;
        if (cardRemain % 4 != 0) return;
        Destroy(topCard.gameObject);
    }

    public void DoActionClick()
    {
        int cardRemain = StreamingResources.Instance.cardDesk.cardList.Count;
        if (cardRemain <= 0) return;

        Card getCard = StreamingResources.Instance.cardDesk.TakeRandomCard();
        StreamingResources.Instance.showingCard.rank = getCard.rank;
        StreamingResources.Instance.showingCard.suitID = getCard.suitID;

        RemoveTopCard();
        DealCard();
    }

    public void DealCard()
    {
        Card currentCard = StreamingResources.Instance.showingCard;
        GameObject current = useCardA ? cardA : cardB;
        GameObject next = useCardA ? cardB : cardA;

        // Load dữ liệu và hiển thị
        current.GetComponent<CardController>()?.LoadSprite(currentCard.rank, currentCard.suitID);
        current.transform.position = transform.position;
        current.transform.rotation = Quaternion.Euler(0, 180, 0);
        current.SetActive(true);

        // Ẩn lá còn lại
        //next.SetActive(false);

        // Di chuyển và xoay
        StartCoroutine(MoveCard(current.transform, showCardPosition, 0.5f));

        useCardA = !useCardA;
    }

    private IEnumerator MoveCard(Transform card, Vector3 targetPos, float duration)
    {
        Vector3 startPos = card.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            card.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            if (elapsed > 0.4f)
            {
                StartCoroutine(FlipCard(card, 0.5f, Quaternion.Euler(0, 0, 0)));
            }
            yield return null;
        }
        GameObject next = !useCardA ? cardB : cardA;
        next.SetActive(false);
        card.position = targetPos;
    }

    private IEnumerator FlipCard(Transform card, float duration, Quaternion targetRotation)
    {
        Quaternion startRotation = card.rotation;
        float elapsed = 0f;
        bool flipped = false;

        GameObject front = card.Find("Front")?.gameObject;
        GameObject back = card.Find("Back")?.gameObject;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            card.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsed += Time.deltaTime;

            float yAngle = card.eulerAngles.y;
            if (!flipped && yAngle > 85f && yAngle < 95f)
            {
                flipped = true;
                bool isTargetFaceUp = Mathf.Abs(targetRotation.eulerAngles.y % 360f) == 0f;
                if (front && back)
                {
                    front.SetActive(isTargetFaceUp);
                    back.SetActive(!isTargetFaceUp);
                }
            }

            yield return null;
        }

        card.rotation = targetRotation;
    }

    private void SetSortingOrder(GameObject card, int order)
    {
        SpriteRenderer[] renderers = card.GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.sortingOrder = order;
        }
    }
}
