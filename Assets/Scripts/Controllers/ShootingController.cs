
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        characterController.OnLookEvent += OnAim;
        characterController.OnRightClickEvent += OnShoot;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;

        float projectileAngleSpace = rangedAttackSO.multipleProjectileAngle;
        int numberOfProjectilePerShot = rangedAttackSO.numberOfProjectilePerShot;

        float minAngle = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace + 0.5f * projectileAngleSpace;
        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + i * projectileAngleSpace;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO attackSO, float angle)
    {
        Transform root = PoolManager.Instance.PoolRoot(Defines.EPoolTarget.Projectile);
        GameObject go = PoolManager.Instance.Spawn(Defines.EPoolTarget.Projectile, root);
        go.GetComponent<ProjectileController>()?.SetData(characterController, RotateVector2(aimDirection, angle), projectileSpawnPosition.position, attackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * v;
    }
}