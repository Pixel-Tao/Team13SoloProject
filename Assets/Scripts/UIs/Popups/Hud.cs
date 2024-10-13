using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : PopupBase
{
    [SerializeField] private TextMeshProUGUI currentTimeText;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        TimeUpdate();
    }

    private void TimeUpdate()
    {
        currentTimeText.text = $"{System.DateTime.Now:HH:ss}";
    }

    public void OnChangeCharacterClick()
    {
        // 캐릭터 바꾸기
        if (UIManager.Instance.IsPopupOpened<CharacterChangePopup>())
        {
            UIManager.Instance.ClosePopup<CharacterChangePopup>();
            return;
        }
        UIManager.Instance.ShowPopup<CharacterChangePopup>();
    }

    public void OnChangeNameClick()
    {
        // 이름 바꾸기
        if (UIManager.Instance.IsPopupOpened<NameChangePopup>())
        {
            UIManager.Instance.ClosePopup<NameChangePopup>();
            return;
        }

        UIManager.Instance.ShowPopup<NameChangePopup>();
    }

    public void OnCheckActiveUsersClick()
    {
        // 참석자
        if (UIManager.Instance.IsPopupOpened<UsersPopup>())
        {
            UIManager.Instance.ClosePopup<UsersPopup>();
            return;
        }

        UIManager.Instance.ShowPopup<UsersPopup>();
    }
}
