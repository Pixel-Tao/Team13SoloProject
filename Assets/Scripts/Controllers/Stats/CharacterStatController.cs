using UnityEngine;

public class CharacterStatController : MonoBehaviour
{
    [SerializeField] private CharacterStatSO baseStat;
    [SerializeField] private AttackSO baseAttack;

    private AnimController animController;
    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;

    public CharacterStat CurrentStat { get; private set; }

    private void Awake()
    {
        animController = GetComponent<AnimController>();
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        InitStat();
    }

    public void InitStat()
    {
        if (baseStat == null)
            return;

        CurrentStat = new CharacterStat();
        CurrentStat.stat = baseStat;
        CurrentStat.attack = baseAttack;
        CurrentStat.maxHealth = baseStat.MaxHealth;
        CurrentStat.speed = baseStat.MoveSpeed;
        GetComponent<HealthController>()?.SetData(CurrentStat);

        if (CurrentStat.stat.GetType() == typeof(NpcStatSO))
        {
            NpcStatSO npcStat = (NpcStatSO)CurrentStat.stat;
            animController.SetLibraryAsset(npcStat.CharacterLibraryAsset);
            characterController.SetCharacterName(npcStat.Name);
            spriteRenderer.color = npcStat.Color;
        }
        else if (CurrentStat.stat.GetType() == typeof(EnemyStatSO))
        {
            EnemyStatSO enemyStat = (EnemyStatSO)CurrentStat.stat;
            animController.SetLibraryAsset(enemyStat.CharacterLibraryAsset);
            characterController.SetCharacterName(enemyStat.Name);
            spriteRenderer.color = enemyStat.Color;
        }
    }
}
