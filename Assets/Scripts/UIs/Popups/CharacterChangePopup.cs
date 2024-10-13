using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterChangePopup : PopupBase
{
    [SerializeField] private CreateCharacterMainPanelUI CharacterMainPanelUI;
    [SerializeField] private CharacterListPanelUI CharacterListPanelUI;

    private PlayerController playerController;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CharacterMainPanelUI.OnOkClickEvent -= OkPressed;
        CharacterMainPanelUI.OnOkClickEvent += OkPressed;
        CharacterMainPanelUI.OnCancelClickEvent -= CancelPressed;
        CharacterMainPanelUI.OnCancelClickEvent += CancelPressed;
        CharacterMainPanelUI.OnCharacterChangeClickEvent -= CharacterChangeClick;
        CharacterMainPanelUI.OnCharacterChangeClickEvent += CharacterChangeClick;

        CharacterListPanelUI.OnCharacterSelected -= CharacterSelected;
        CharacterListPanelUI.OnCharacterSelected += CharacterSelected;

        ShowMainPanel(true);
    }

    private void CharacterSelected(SpriteLibraryAsset asset)
    {
        CharacterMainPanelUI?.SetCharacter(asset);
        ShowMainPanel(true);
    }

    private void CharacterChangeClick()
    {
        ShowMainPanel(false);
    }

    private void ShowMainPanel(bool isMainPanel)
    {
        CharacterMainPanelUI.gameObject.SetActive(isMainPanel);
        CharacterListPanelUI.gameObject.SetActive(!isMainPanel);
    }

    private void OkPressed(string name, SpriteLibraryAsset asset)
    {
        if (playerController == null)
        {
            GameObject go = PoolManager.Instance.Spawn(Defines.EPoolTarget.Player);
            playerController = go.GetComponent<PlayerController>();
        }

        playerController.SetCharacterName(name);
        playerController.GetComponent<AnimController>().SetLibraryAsset(asset);
        GameManager.Instance.SetPlayer(playerController);
        UIManager.Instance.ClosePopup(gameObject);
    }

    private void CancelPressed()
    {
        UIManager.Instance.ClosePopup(gameObject);
    }

    private void SetPlayer(PlayerController player = null)
    {
        this.playerController = player;
        if (player != null)
        {
            SpriteLibrary library = player.GetComponent<AnimController>()?.SpriteLibrary;
            CharacterMainPanelUI.SetData(player.CharacterName, library.spriteLibraryAsset);
        }
        else
        {
            CharacterMainPanelUI.SetData();
        }
    }

    public override void Show()
    {
        UIManager.Instance.CloseAllPopup(this);
        GameManager.Instance.SetPlayerActive(false);
        SetPlayer(GameManager.Instance.Player);
        base.Show();
    }

    public override void Close()
    {
        base.Close();
        UIManager.Instance.ShowPopup<Hud>();
    }
}
