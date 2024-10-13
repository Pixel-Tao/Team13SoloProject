
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatSO", menuName = "SoloProject/EnemyStatSO", order = 0)]
public class EnemyStatSO : CharacterStatSO
{
    public string Name;
    [ColorUsage(true, true)] public Color Color;
    public SpriteLibraryAsset CharacterLibraryAsset;
}