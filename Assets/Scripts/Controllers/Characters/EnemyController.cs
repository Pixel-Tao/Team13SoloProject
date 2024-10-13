using UnityEngine;

public class EnemyController : CharacterController
{
    private HealthController healthController;

    private Transform attacker;
    private Vector2 moveDirection;

    private HealthController targetHealth;
    private bool isCollidingWithTarget;

    protected override void Awake()
    {
        base.Awake();
        healthController = GetComponent<HealthController>();
    }

    private void Update()
    {
        if (isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        if (attacker != null)
        {
            FollowAttacker();
        }
        else if (moveDirection != Vector2.zero)
        {
            MoveUpdate();
        }
    }

    private void Start()
    {
        if (detectController != null)
        {
            detectController.SetLayerMask(Defines.ELayerMask.Player);
            detectController.OnDetected -= Detected;
            detectController.OnDetected += Detected;
        }

        if (healthController != null)
        {
            healthController.OnDamage += OnDamage;
        }
    }

    public void Init()
    {
        attacker = null;
        moveDirection = Vector2.zero;
        targetHealth = null;
        statController.InitStat();

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = true;
        }
    }

    private void FollowAttacker()
    {
        if (attacker == null) return;

        moveDirection = DirectionToTarget(attacker.position);
        MoveUpdate();
    }
    private void OnDamage(CharacterController attacker)
    {
        // 데미지 받으면 그냥 계속 쫓아옴
        this.attacker = attacker.transform;
    }

    private void Detected(Transform target)
    {
        if (attacker != null) return;

        if (target == null)
        {
            moveDirection = Vector2.zero;
            Move(moveDirection);
            Look(moveDirection);
            return;
        }

        moveDirection = DirectionToTarget(target.position);
    }

    private void MoveUpdate()
    {
        Move(moveDirection);
        Look(moveDirection);
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSO = statController.CurrentStat.attack;
        if (attackSO == null) return;

        bool isAttackable = targetHealth.ChangeHealth(this, -attackSO.power);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;
        if (receiver.layer == (int)Defines.ELayerMask.Player)
        {
            HealthController health = collision.GetComponent<HealthController>();
            if (health != null)
            {
                isCollidingWithTarget = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;
        if (receiver.layer == (int)Defines.ELayerMask.Player)
            isCollidingWithTarget = false;
    }

    private Vector2 DirectionToTarget(Vector3 target)
    {
        return (target - transform.position).normalized;
    }
}
