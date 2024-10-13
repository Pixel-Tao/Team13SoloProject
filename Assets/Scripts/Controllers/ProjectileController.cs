using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    private Vector2 direction = Vector2.right;
    private RangedAttackSO rangedAttackSO;
    private CharacterController owner;

    private float currentDuration;

    private Rigidbody2D rigidbody;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;

    private bool isReady;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isReady == false) return;

        currentDuration += Time.deltaTime;
        if (currentDuration >= rangedAttackSO.duration)
        {
            DestroyProjectile();
        }

        rigidbody.velocity = direction * rangedAttackSO.speed;
    }

    public void SetData(CharacterController owner, Vector2 direction, Vector2 position, RangedAttackSO attackSO)
    {
        transform.position = position;
        this.owner = owner;
        this.direction = direction;
        this.rangedAttackSO = attackSO;

        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.sprite = attackSO.sprite;
        spriteRenderer.color = attackSO.projectileColor;
        transform.right = this.direction;
        isReady = true;
    }

    private void DestroyProjectile()
    {
        if (isReady == false) return;

        PoolManager.Instance.Despawn(Defines.EPoolTarget.Projectile, gameObject);
        isReady = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(collision.gameObject.layer, rangedAttackSO.target))
        {
            // TODO : 데미지
            HealthController health = collision.GetComponent<HealthController>();
            if(health != null)
            {
                health.ChangeHealth(owner, -rangedAttackSO.power);
            }
            DestroyProjectile();
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | (1 << layer));
    }
}
