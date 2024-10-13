
using System.Collections;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthController healthController;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        healthController.OnDeath += Destroy;
    }

    private void DestorySelf()
    {
        PoolManager.Instance.Despawn(Defines.EPoolTarget.Enemy, gameObject);
    }

    private void Destroy()
    {
        rigidbody.velocity = Vector2.zero;
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = false;
        }

        Invoke("DestorySelf", 1f);
    }
}