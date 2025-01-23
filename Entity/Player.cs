using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : Character
{
    public Player(string name, int health, int attackPower, int defensePower, float attckSpeed, float attackDistance)
        : base(name, health, attackPower, defensePower, attckSpeed, attackDistance) { }

    private static Player _instance;
    public static Player Instance { get => _instance ?? (_instance = FindFirstObjectByType<Player>()); }

    [SerializeField] Vector3 centerOffset;
    public Func<float, Character> OnDetact { get; set; }

    protected override void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this as Player;

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        info.state = CharacterState.Attack;

        OnDetact = (dist) =>
        {
            var nearEnemy = EnemyManager.Instance.GetNearEnemy();
            if (nearEnemy == null)
                return null;

            if (dist >= nearEnemy.GetPosition().x - GetPosition().x)
                return nearEnemy;

            return null;
        };
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + centerOffset, new Vector3(9f, 9f, 0f));
    }
#endif
}
