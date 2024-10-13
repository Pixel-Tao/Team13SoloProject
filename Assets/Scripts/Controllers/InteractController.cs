using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private CharacterController characterController;
    private AnimController animController;

    private bool isHovering;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animController = GetComponent<AnimController>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        characterController.OnLeftClickEvent += Interact;
    }

    private void Update()
    {
        ApplyHover();
    }

    private void ApplyHover()
    {
        if (animController != null)
        {
            animController.HoverAnim(isHovering);
        }
    }

    private void Interact()
    {
        // 왼쪽 마우스 클릭 당했을때
    }

    public void MouseOver()
    {
        isHovering = true;
    }

    public void MouseLeave()
    {
        isHovering = false;
    }
}
