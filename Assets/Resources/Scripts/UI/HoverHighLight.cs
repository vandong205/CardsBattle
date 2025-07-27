using UnityEngine;
using UnityEngine.InputSystem;

public class DeskHoverHighlighter : MonoBehaviour
{
    public Color normalColor = Color.gray;
    public Color highlightColor = Color.white;
    public Vector3 hoverOffset = new Vector3(0.1f, 0.1f, 0); // Lệch lên và phải
    public float moveSpeed = 12f; // Tốc độ dịch chuyển
    public float colorLerpSpeed = 14f; // Tốc độ đổi màu

    private Renderer[] renderers;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private Color currentColor;
    private Color targetColor;

    void Start()
    {
        RefreshRenderers();
        renderers = GetComponentsInChildren<Renderer>();
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;

        // Clone material để mỗi desk có vật liệu riêng
        foreach (var r in renderers)
        {
            r.material = new Material(r.material);
            r.material.color = normalColor;
        }

        currentColor = normalColor;
        targetColor = normalColor;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        bool isHovering = hit.collider != null &&
                          (hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform));

        // Cập nhật mục tiêu
        targetPosition = isHovering ? originalPosition + hoverOffset : originalPosition;
        targetColor = isHovering ? highlightColor : normalColor;

        // Nội suy vị trí
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);

        // Nội suy màu
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * colorLerpSpeed);
        SetColor(currentColor);
    }

    void SetColor(Color color)
    {
        foreach (var r in renderers)
        {
            if (r.material.HasProperty("_Color"))
                r.material.color = color;
        }
    }
    void OnTransformChildrenChanged()
    {
        RefreshRenderers();
    }

    public void RefreshRenderers()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }
}
