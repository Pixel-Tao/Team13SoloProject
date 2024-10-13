using System;
using UnityEngine;

public class DetectController : MonoBehaviour
{
    public event Action<Transform> OnDetected;

    private LayerMask targetLayerMask = 0;
    [SerializeField][Range(0f, 100f)] private float detectRadius;

    private void Awake()
    {

    }
    public void SetLayerMask(params Defines.ELayerMask[] layers)
    {
        targetLayerMask = Util.CombineLayersToMask(layers);
    }

    private void Update()
    {
        Detect(transform.position, detectRadius);
    }

    public void Detect(Vector2 position, float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, detectRadius, targetLayerMask);
        OnDetected?.Invoke(collider?.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
