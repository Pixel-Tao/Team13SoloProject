using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public PlayerController Player { get; private set; }

    public HashSet<CharacterController> Users { get; private set; } = new HashSet<CharacterController>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
        SetPlayerActive(true);
        virtualCamera.Follow = player.transform;
        AddUser(player);
    }

    public void SetPlayerActive(bool active)
    {
        Player?.gameObject.SetActive(active);
    }

    public void SetPlayerName(string name)
    {
        Player?.SetCharacterName(name);
    }

    public void AddUser(CharacterController user)
    {
        if (Users.Contains(user)) 
            return;

        Users.Add(user);
    }
}
