using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NpcDialogPopup : PopupBase
{
    private struct MessageQueueItem
    {
        public string message;
        public bool isLeft;
    }

    private NpcController npc;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject messageItemPrefab;
    [SerializeField] private ScrollRect scrollView;

    private Queue<MessageQueueItem> dialogQueue = new Queue<MessageQueueItem>();

    private void Update()
    {
        
    }

    public void OnTalkClick()
    {
        if (npc == null)
            return;

        if (dialogQueue.Count > 0)
            return;

        dialogQueue.Enqueue(GenMsgItem("심심해요. 놀아줘요.", false));

        string[] items = npc.GetMessages();
        foreach (string item in items)
            dialogQueue.Enqueue(GenMsgItem(item));

        StartCoroutine(WriteMessage());
    }

    public void SetNpc(Transform target)
    {
        npc = target.GetComponent<NpcController>();
        if (npc == null)
            return;

        ClearMessage();

        nameText.text = npc.CharacterName;

        dialogQueue.Enqueue(GenMsgItem("안녕하세요?"));
        dialogQueue.Enqueue(GenMsgItem($"{npc.CharacterName} 입니다."));
        dialogQueue.Enqueue(GenMsgItem("무엇을 도와드릴까요?"));
        StartCoroutine(WriteMessage());
    }

    IEnumerator WriteMessage()
    {
        while (IsOpen && dialogQueue.Count > 0)
        {
            MessageQueueItem item = dialogQueue.Dequeue();

            GameObject messageItem = Instantiate(messageItemPrefab, content);
            messageItem.GetComponent<MessageItemUI>().SetMessage(item.message, item.isLeft);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            yield return null;
            scrollView.verticalNormalizedPosition = 0f;
            yield return new WaitForSeconds(1f);
        }
    }

    private void ClearMessage()
    {
        dialogQueue.Clear();

        foreach (Transform child in content)
            Destroy(child.gameObject);
    }

    private MessageQueueItem GenMsgItem(string msg, bool isLeft = true)
    {
        return new MessageQueueItem { message = msg, isLeft = isLeft };
    }
}
