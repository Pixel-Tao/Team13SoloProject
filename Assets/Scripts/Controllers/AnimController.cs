using UnityEngine;
using UnityEngine.U2D.Animation;

public class AnimController : MonoBehaviour
{
    private static readonly int isRun = Animator.StringToHash("isRun");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int attack = Animator.StringToHash("attack");
    private static readonly int isHovered = Animator.StringToHash("isHovered");

    public SpriteLibrary SpriteLibrary { get; private set; }

    private Animator animator;
    private CharacterController characterController;
    private HealthController healthController;

    private readonly float magnitudeThreshold = 0.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        healthController = GetComponent<HealthController>();
        SpriteLibrary = GetComponentInChildren<SpriteLibrary>();
    }

    private void Start()
    {
        characterController.OnMoveEvent += MoveAnim;
        characterController.OnRightClickEvent += AttackAnim;
        if (healthController != null)
        {
            healthController.OnInvincibilityEnd += InvincibilityEndAnim;
            healthController.OnDamage += HitAnim;
        }
    }

    private void AttackAnim(AttackSO attackSO)
    {
        animator.SetTrigger(attack);
    }

    private void MoveAnim(Vector2 direction)
    {
        animator.SetBool(isRun, direction.magnitude > magnitudeThreshold);
    }

    public void HitAnim(CharacterController attacker)
    {
        animator.SetBool(isHit, true);
    }

    public void InvincibilityEndAnim()
    {
        animator.SetBool(isHit, false);
    }

    public void HoverAnim(bool isHovering)
    {
        animator.SetBool(isHovered, isHovering);
    }

    public void SetLibraryAsset(SpriteLibraryAsset spriteLibraryAsset)
    {
        SpriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
    }
}
