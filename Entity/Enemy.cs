
public class Enemy : Character
{
    public Enemy(string name, int health, int attackPower, int defensePower, float attckSpeed, float attackDistance)
        : base(name, health, attackPower, defensePower, attckSpeed, attackDistance) { }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        info.state = CharacterState.Attack;

        onGetHit += (damage, pos) =>
        {
            DamagePooling.Instance.SetDamage(damage, pos);
        };

        onDeath += () =>
        {
            GameManager.Instance.frontUI.ShowAddEvent(UnityEngine.Random.Range(50,300), GetPosition());
            bool death = EnemyManager.Instance.OnDeath(this);
        };
    }
}
