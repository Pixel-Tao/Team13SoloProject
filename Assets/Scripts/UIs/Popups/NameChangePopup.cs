using UnityEngine;

public class NameChangePopup : PopupBase
{
    [SerializeField] private NameChangePanelUI nameChangePanelUI;

    // Start is called before the first frame update
    void Start()
    {
        nameChangePanelUI.OnOkClickEvent -= OkPressed;
        nameChangePanelUI.OnOkClickEvent += OkPressed;
        nameChangePanelUI.OnCancelClickEvent -= CancelPressed;
        nameChangePanelUI.OnCancelClickEvent += CancelPressed;
    }

    private void OkPressed(string name)
    {
        GameManager.Instance.SetPlayerName(name);
        UIManager.Instance.ClosePopup(gameObject);
    }

    private void CancelPressed()
    {
        UIManager.Instance.ClosePopup(gameObject);
    }

    public override void Show()
    {
        nameChangePanelUI.SetValue(GameManager.Instance.Player?.CharacterName);
        base.Show();
    }
}
