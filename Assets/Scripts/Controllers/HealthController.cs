

using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action<CharacterController> OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    private CharacterStat currentStat;
    public float CurrentHealth { get; private set; }
    public float MaxHealth => currentStat.maxHealth;

    private CharacterController attacker;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void SetData(CharacterStat currentStat)
    {
        this.currentStat = currentStat;
        CurrentHealth = currentStat.maxHealth;
        timeSinceLastChange = float.MaxValue;
        isAttacked = false;
    }

    private void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(CharacterController attacker, float amount)
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            // 공격을 하지 않고 끝나는 상황
            return false;
        }
        this.attacker = attacker;
        timeSinceLastChange = 0;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            CallDeath();
            return true;
        }
        if (amount >= 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke(attacker);
            isAttacked = true;
        }

        return true;
    }


    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

}