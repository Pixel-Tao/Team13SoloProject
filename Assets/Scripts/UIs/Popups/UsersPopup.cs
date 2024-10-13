using UnityEngine;

public class UsersPopup : PopupBase
{
    [SerializeField] private Transform content;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UIs/UserListItemUI");
        foreach (var user in GameManager.Instance.Users)
        {
            GameObject go = Instantiate(prefab, content);
            go.name = "Item";
            UserListItemUI userListItemUI = go.GetComponent<UserListItemUI>();
            userListItemUI.SetUser(user);
        }
    }

    public void OnCloseButtonClick()
    {
        UIManager.Instance.ClosePopup<UsersPopup>();
    }

}
