using System;
using UnityEngine;

public class NpcController : CharacterController
{
    private Vector2 initPosition;
    private Vector2 movePosition;
    private Vector2 moveDirection;

    private float randomMoveTime = 5f;

    protected override void Awake()
    {
        base.Awake();

        initPosition = transform.position;
    }

    private void Start()
    {
        GameManager.Instance.AddUser(this);
        randomMoveTime = UnityEngine.Random.Range(3f, 10f);
    }

    private void Update()
    {
        randomMoveTime -= Time.deltaTime;
        if(randomMoveTime <= 0)
        {
            RandomPosition();
        }

        if(movePosition != Vector2.zero)
        {
            MoveUpdate();
        }
    }

    private void RandomPosition()
    {
        float x = UnityEngine.Random.Range(1f, 5f);
        float y = UnityEngine.Random.Range(1f, 5f);
        movePosition = new Vector2(initPosition.x + x, initPosition.y + y);
        moveDirection = (movePosition - (Vector2)transform.position).normalized;
        randomMoveTime = UnityEngine.Random.Range(3f, 10f);
    }

    private void MoveUpdate()
    {
        Move(moveDirection);
        Look(moveDirection);
        if (Vector2.Distance(transform.position, movePosition) < 0.1f)
        {
            moveDirection = Vector2.zero;
            movePosition = Vector2.zero;
            Move(moveDirection);
        }
    }

    public string[] GetMessages()
    {
        if(statController.CurrentStat.stat.GetType() == typeof(NpcStatSO))
        {
            NpcStatSO npcStat = (NpcStatSO)statController.CurrentStat.stat;
            return npcStat.Messages.ToArray();
        }

        return Array.Empty<string>();
    }
}
