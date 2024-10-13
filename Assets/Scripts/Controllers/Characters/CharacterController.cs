using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnLeftClickEvent;
    public event Action<AttackSO> OnRightClickEvent;

    public event Action<bool> OnLogInOutEvent;
    public event Action<string> OnNameChanged;

    public Rigidbody2D rigidbody;
    protected DetectController detectController;
    protected CharacterStatController statController;

    [SerializeField] private string characterName;
    public string CharacterName => characterName;

    private CharacterNameUI nameUI;

    public bool IsLogin => gameObject.activeSelf;
    public bool IsAttacking { get; private set; }

    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        nameUI = GetComponentInChildren<CharacterNameUI>();
        detectController = GetComponent<DetectController>();
        statController = GetComponent<CharacterStatController>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (statController.CurrentStat.attack == null) return;

        if (timeSinceLastAttack < statController.CurrentStat.attack.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if(IsAttacking && timeSinceLastAttack >= statController.CurrentStat.attack.delay)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(statController.CurrentStat.attack);
        }
    }

    public void SetCharacterName(string name)
    {
        this.characterName = name;
        nameUI.SetName(name);
        OnNameChanged?.Invoke(name);
    }

    public void Move(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void Look(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void Interact(Transform transform)
    {
        OnLeftClickEvent?.Invoke();
    }

    public void Attack(bool isPressed)
    {
        IsAttacking = isPressed;
    }

    private void CallAttackEvent(AttackSO attack)
    {
        OnRightClickEvent?.Invoke(attack);
    }

    public void Login()
    {
        gameObject.SetActive(true);
        OnLogInOutEvent?.Invoke(true);
    }

    public void Logout()
    {
        gameObject.SetActive(false);
        OnLogInOutEvent?.Invoke(false);
    }
}
