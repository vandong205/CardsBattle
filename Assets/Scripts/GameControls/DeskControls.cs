using UnityEngine;

public class DeskControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int numberOfCards = 52;
    [SerializeField] float offsety = 0.1f;
    [SerializeField] GameObject CardsbackPrefabs;
    void Start()
    {
        CreateVisualDesk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateVisualDesk()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 offset = new Vector3(0, i * offsety, -i * 0.001f);
            Instantiate(CardsbackPrefabs, transform.position + offset, Quaternion.identity, transform);
        }
    }
    public void RemoveTopCard()
    {
        if (transform.childCount == 0) return;

        Transform topCard = transform.GetChild(transform.childCount - 1);
        Destroy(topCard.gameObject);
    }
}
