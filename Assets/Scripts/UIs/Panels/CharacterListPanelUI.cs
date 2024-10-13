using System;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class CharacterListPanelUI : UIBase
{
    public event Action<SpriteLibraryAsset> OnCharacterSelected;

    [SerializeField] private SelecteableCharacterSO selectableCharacter;

    [SerializeField] private RectTransform content;
    [SerializeField] private ScrollRect scrollView;

    [SerializeField] private GameObject characterItemPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        selectableCharacter.Items.ForEach(item =>
        {
            GameObject obj = Instantiate(characterItemPrefab, content);
            obj.name = "Item";
            CharacterItemUI characterItemUI = obj.GetComponent<CharacterItemUI>();
            characterItemUI.SetItem(item);
            characterItemUI.OnItemClickEvent += OnCharacterItemClick;
        });

        scrollView.horizontalNormalizedPosition = 0f;
    }

    private void OnCharacterItemClick(SpriteLibraryAsset asset)
    {
        OnCharacterSelected?.Invoke(asset);
    }
}
