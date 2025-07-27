using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Camera _maincamera;
    float clickDelay = 1.0f;
    private float lastClickTime = -Mathf.Infinity;
    private void Awake()
    {
        _maincamera = Camera.main;
    }
    public void onClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (Time.time - lastClickTime < clickDelay) return;
        var rayHit = Physics2D.GetRayIntersection(_maincamera.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        var clickable = rayHit.collider.gameObject.GetComponent<IClickable>();
        if (clickable != null)
        {
            clickable.DoActionClick();
        }
        lastClickTime = Time.time;
    }
}
