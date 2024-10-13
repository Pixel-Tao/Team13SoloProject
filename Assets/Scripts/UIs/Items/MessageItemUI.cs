using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageItemUI : UIBase
{
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private bool isLeft = true;

    private VerticalLayoutGroup layoutGroup;

    protected override void Awake()
    {
        base.Awake();

        layoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    private void Start()
    {
        
    }

    public void SetMessage(string message, bool isLeft = true)
    {
        this.isLeft = isLeft;
        messageText.text = message;
        layoutGroup.childAlignment = isLeft ? TextAnchor.UpperLeft : TextAnchor.UpperRight;
    }
}
