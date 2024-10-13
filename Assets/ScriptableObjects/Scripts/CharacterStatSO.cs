
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatSO", menuName = "SoloProject/DefaultCharacterSO", order = 0)]
public class CharacterStatSO : ScriptableObject
{
    [Range(1, 1000)] public int MaxHealth;
    [Range(1f, 20f)] public float MoveSpeed;
}