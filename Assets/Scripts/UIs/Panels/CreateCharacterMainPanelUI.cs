using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class CreateCharacterMainPanelUI : UIBase
{
    public event Action<string, SpriteLibraryAsset> OnOkClickEvent;
    public event Action OnCancelClickEvent;
    public event Action OnCharacterChangeClickEvent;

    [SerializeField] private NameChangePanelUI nameChangePanelUI;

    [SerializeField] private Image selectedCharacterImage;

    [SerializeField] private SpriteLibraryAsset selectedCharacter;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        nameChangePanelUI.OnOkClickEvent -= OkPressed;
        nameChangePanelUI.OnOkClickEvent += OkPressed;
        nameChangePanelUI.OnCancelClickEvent -= CancelPressed;
        nameChangePanelUI.OnCancelClickEvent += CancelPressed;
    }

    private void OkPressed(string name)
    {
        OnOkClickEvent?.Invoke(name, selectedCharacter);
    }

    private void CancelPressed()
    {
        OnCancelClickEvent?.Invoke();
    }

    public void OnCharacterChangeClick()
    {
        // 캐릭터 선택 클릭
        OnCharacterChangeClickEvent?.Invoke();
    }

    public void SetData(string name, SpriteLibraryAsset asset)
    {
        if (string.IsNullOrWhiteSpace(name) || asset == null)
            return;

        nameChangePanelUI.SetValue(name);
        SetCharacter(asset);
    }

    public void SetData()
    {
        // 캐릭터 선택
        nameChangePanelUI.SetValue();
        SetCharacter(selectedCharacter);
    }

    public void SetCharacter(SpriteLibraryAsset asset)
    {
        if (selectedCharacter != asset) selectedCharacter = asset;
        selectedCharacterImage.sprite = selectedCharacter.GetSprite("Idle", "idle_f0");
    }
}
