using UnityEngine;

[System.Serializable]
public class CharacterStat
{
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public CharacterStatSO stat;
    public AttackSO attack;
}
