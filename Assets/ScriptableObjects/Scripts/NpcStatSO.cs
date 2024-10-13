
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcStatSO", menuName = "SoloProject/NpcStatSO", order = 0)]
public class NpcStatSO : CharacterStatSO
{
    public string Name;
    [ColorUsage(true, true)] public Color Color;
    public SpriteLibraryAsset CharacterLibraryAsset;

    public List<string> Messages;
}