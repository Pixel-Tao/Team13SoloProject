using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameChangePanelUI : UIBase
{
    public event Action<string> OnOkClickEvent;
    public event Action OnCancelClickEvent;

    [SerializeField] private TMP_InputField characterNameInputText;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button okButton;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OnOkClick()
    {
        // 캐릭터 이름 변경 완료 클릭
        OnOkClickEvent?.Invoke(characterNameInputText.text);
    }

    public void OnCancelClick()
    {
        // 캐릭터 이름 변경 취소 클릭
        OnCancelClickEvent?.Invoke();
    }

    public void SetValue(string name = "")
    {
        cancelButton.enabled = string.IsNullOrWhiteSpace(name) == false;

        characterNameInputText.text = name;
    }
}