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
    [SerializeField] private TextMeshProUGUI messageText;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        messageText.text = String.Empty;
    }

    public void OnOkClick()
    {
        // 캐릭터 이름 변경 완료 클릭
        string name = characterNameInputText.text;
        if (string.IsNullOrEmpty(name) || name.Length < 2 || name.Length > 10)
        {
            // 이름이 2자 이상 10자 이하가 아니면
            messageText.text = "이름은 2자 이상 10자 이하로 입력해주세요.";
            return;
        }

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