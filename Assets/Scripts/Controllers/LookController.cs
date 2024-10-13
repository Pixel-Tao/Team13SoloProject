using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour
{
    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        characterController.OnLookEvent += Look;
    }

    private void Look(Vector2 direction)
    {
        spriteRenderer.flipX = direction.x < 0;
    }
}
