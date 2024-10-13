using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class CharacterItemUI : UIBase
{
    public event Action<SpriteLibraryAsset> OnItemClickEvent;

    private SpriteLibraryAsset spriteLibraryAsset;

    public Image Image;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetItem(SpriteLibraryAsset item)
    {
        spriteLibraryAsset = item;
        Image.sprite = spriteLibraryAsset.GetSprite("Idle", "idle_f0");
    }

    public void OnCharacterItemClick()
    {
        OnItemClickEvent?.Invoke(spriteLibraryAsset);
    }
}
