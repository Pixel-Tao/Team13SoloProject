using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "SoloProject/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberOfProjectilePerShot;
    public float multipleProjectileAngle;
    public Color projectileColor;
}