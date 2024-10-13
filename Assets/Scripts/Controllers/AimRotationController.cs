

using UnityEngine;

public class AimRotationController : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;
    [SerializeField] private SpriteRenderer weaponRenderer;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        characterController.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        Rotate(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        aimPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}