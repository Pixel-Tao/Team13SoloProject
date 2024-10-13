using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private CharacterController characterController;
    private Camera camera;

    private Vector2 worldPosition = Vector2.zero;
    private Transform mouseOverTranform;

    private LayerMask rayTargetMask = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
        rayTargetMask = Util.CombineLayersToMask(
        Defines.ELayerMask.Player,
        Defines.ELayerMask.Enemy,
        Defines.ELayerMask.Npc
        );
    }

    public void OnLeftClick(InputValue value)
    {
        // 마우스 왼쪽 클릭
        if (value.isPressed)
            characterController.Interact(mouseOverTranform);
    }

    public void OnMove(InputValue value)
    {
        // 플레이어 이동
        Vector2 moveInput = value.Get<Vector2>().normalized;
        characterController.Move(moveInput);
    }

    public void OnLook(InputValue value)
    {
        // 마우스 바라보기
        Vector2 newAim = value.Get<Vector2>();
        worldPosition = camera.ScreenToWorldPoint(newAim);
        newAim = (worldPosition - (Vector2)transform.position).normalized;

        // 오브젝트 대상 마우스 올렸는지 확인
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0.0f, rayTargetMask);
        mouseOverTranform?.GetComponent<InteractController>()?.MouseLeave();
        if (hit.collider != null)
        {
            mouseOverTranform = hit.collider.gameObject.transform;
            mouseOverTranform?.GetComponent<InteractController>()?.MouseOver();
        }
        else
        {
            mouseOverTranform = null;
        }

        characterController.Look(newAim);
    }

    public void OnRightClick(InputValue value)
    {
        characterController.Attack(value.isPressed);
    }
}
