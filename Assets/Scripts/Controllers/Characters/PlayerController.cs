using UnityEngine;

public class PlayerController : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (detectController != null)
        {
            detectController.SetLayerMask(Defines.ELayerMask.Enemy, Defines.ELayerMask.Npc);
            detectController.OnDetected -= Detected;
            detectController.OnDetected += Detected;
        }
    }

    private void Detected(Transform target)
    {
        if (target == null)
        {
            UIManager.Instance.ClosePopup<NpcDialogPopup>();
            return;
        }

        CharacterController character = target.GetComponent<CharacterController>();
        if (character.GetType() != typeof(NpcController))
            return;

        if (UIManager.Instance.IsPopupOpened<NpcDialogPopup>() == false)
        {
            NpcDialogPopup ui = UIManager.Instance.ShowPopup<NpcDialogPopup>();
            ui.SetNpc(target);
        }
    }
}
