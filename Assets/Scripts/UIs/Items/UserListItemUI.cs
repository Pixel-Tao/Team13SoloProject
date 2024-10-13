using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserListItemUI : UIBase
{
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private Button statusButton;
    [SerializeField] private TextMeshProUGUI statusText;

    private CharacterController user;

    private readonly Color loginColor = new Color(0, 200, 0);
    private readonly Color logoutColor = new Color(200, 0, 0);

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetUser(CharacterController user)
    {
        this.user = user;
        userNameText.text = user.CharacterName;
        StatusUpdate(user.IsLogin);
        if (user.GetType() == typeof(PlayerController))
        {
            statusButton.enabled = false;
            user.OnNameChanged -= OnPlayerNameChanged;
            user.OnNameChanged += OnPlayerNameChanged;
        }

        user.OnLogInOutEvent -= StatusUpdate;
        user.OnLogInOutEvent += StatusUpdate;

        user.Login();
    }

    private void OnPlayerNameChanged(string name)
    {
        userNameText.text = name;
    }

    public void StatusUpdate(bool isLogin)
    {
        statusText.text = isLogin ? "Login" : "Logout";
        Image buttonImage = statusButton.GetComponent<Image>();
        buttonImage.color = isLogin ? loginColor : logoutColor;
    }

    public void OnStatusButtonClick()
    {
        if (user.GetType() == typeof(PlayerController))
            return;

        if (user.IsLogin)
        {
            user.Logout();
        }
        else
        {
            user.Login();
        }
    }
}
