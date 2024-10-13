using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "SelecteableCharacter", menuName = "SoloProject/SelecteableCharacter", order = 0)]
public class SelecteableCharacterSO : ScriptableObject
{
    public List<SpriteLibraryAsset> Items;
}
