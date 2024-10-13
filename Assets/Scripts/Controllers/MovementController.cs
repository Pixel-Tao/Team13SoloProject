using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CharacterController characterController;
    private CharacterStatController statController;
    private Rigidbody2D rigidbody;

    private Vector2 direction = Vector2.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        statController = GetComponent<CharacterStatController>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        characterController.OnMoveEvent -= Move;
        characterController.OnMoveEvent += Move;
    }

    private void OnDestroy()
    {
        characterController.OnMoveEvent -= Move;
    }

    private void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        ApplyMovement(direction);
    }

    private void ApplyMovement(Vector2 movementDirection)
    {
        rigidbody.velocity = movementDirection * statController.CurrentStat.speed;
    }
}
