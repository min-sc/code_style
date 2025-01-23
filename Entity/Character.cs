using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterInfo info;
    public StateMachine StateMachine { get; private set; }

    protected Animator anim;
    protected SpriteRenderer spRen;
    protected Rigidbody2D rb;
    protected WeaponSpanwer ws;

    public Action<int, Vector2> onGetHit;
    public Action onDeath;

    protected CharacterState curState;
    protected float moveSpeed;

    private HPbar hpBar;
    private Character findEnemy;
    private int currentHp;
    private Vector2 hitPoint;
    private Vector2 hitNormal;
    private int hitDamage;
    private bool endTurn;

    public Vector2 SkillSpawnPos => transform.Find("spawn_skill").position;
    public Transform HealSpawn => transform.Find("spawn_heal");

    public Character(string name, int health, int attackPower, int defensePower, float attackSpeed, float attackDistance)
    {
        info = new CharacterInfo(name, health, attackPower, defensePower, attackSpeed, attackDistance);
        curState = CharacterState.Idle;
    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        spRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ws = GetComponent<WeaponSpanwer>();

        var hpPos = transform.Find("hp_pos");
        if (hpPos != null)
        {
            hpBar = hpPos.gameObject.AddComponent<HPbar>();
            hpBar.SetHp(info.health);
        }

        currentHp = info.health;
        endTurn = false;

        StateMachine = new StateMachine();
        StateMachine.AddState(new IdleState(this));
        StateMachine.AddState(new WalkState(this));
        StateMachine.AddState(new AttackState(this, null, () => { StartCoroutine(RangeAttackAndWait()); }));
    }

    protected virtual void Start()
    {
        onGetHit = (damage, pos) =>
        {
            StartCoroutine(SetHitColor());
            hpBar?.onHit(damage);
        };

        onDeath = () =>
        {
            StopAllCoroutines();
            hpBar?.Death();
        };

        StateMachine.Initialize(EState.Idle);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "bullet")
            return;

        Weapon wp = collision.gameObject.GetComponent<Weapon>();

        if (wp?.FollowType != info.unitType)
            return;

        var contact = collision.GetContact(0);
        hitPoint = contact.point;
        hitNormal = contact.normal;
        hitDamage = wp.Damage;

        Defend();
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }

    public void DirectAttack(Character target)
    {
        int damage = info.attackPower - target.info.defensePower;
        damage = damage < 0 ? 0 : damage; 
        target.info.health -= damage;
    }
    
    public void Move()
    {
        transform.Translate((info.unitType == UnitType.Player ? transform.right : transform.right * -1f) * info.moveSpeed * Time.deltaTime);
    }

    public IEnumerator RangeAttackAndWait()
    {
        if (endTurn || !FindEnemyOnDistance()) 
            yield break;

        RangeAttack();
        endTurn = true;

        yield return new WaitForSeconds(info.attackSpeed);

        endTurn = false;
    }

    public void RangeAttack()
    {
        ws.SpawnRange(new AttackInfo(info.attackPower, info.attackDistance, info.attackSpeed, findEnemy.transform));
    }

    public bool FindEnemyOnDistance()
    {
        findEnemy = info.unitType == UnitType.Player ? EnemyManager.Instance.OnDetact(info.attackDistance) : IsInEnemyDis(Player.Instance.GetPosition()) ? Player.Instance : null;

        return findEnemy != null ? true : false;
    }

    public Character[] FindEnemyAll()
    {
        return EnemyManager.Instance.GetAllEnemy();
    }

    public void Defend()
    {
        int reducedDamage = hitDamage - info.defensePower;
        reducedDamage = reducedDamage < 0 ? 0 : reducedDamage;
        currentHp -= reducedDamage;

        onGetHit(reducedDamage, hitPoint);

        if (currentHp <= 0)
            Die();

        GetHitPush(hitNormal);
    }

    public void Defend(int damage)
    {
        currentHp -= damage;

        onGetHit(damage, hitPoint);

        if (currentHp <= 0)
            Die();

        GetHitPush(hitNormal);
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        hpBar?.onHit(-amount);
    }

    public void AddStrong(int amount)
    {
        info.attackPower += amount;
    }

    public void AddDefend(int amount)
    {
        info.defensePower += amount;
    }
    
    public void AddGold(int amount)
    {
        int tempGold = 100;//TODO make rule
        GameManager.Instance.frontUI.ShowAddEvent(UnityEngine.Random.Range(tempGold, amount), GetPosition());
    }

    public void Die()
    {
        onDeath();
        Destroy(this.gameObject);
    }

    public bool IsAlive()
    {
        return info.health > 0;
    }
    
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Vector2 GetLocalPosition()
    {
        return transform.localPosition;
    }

    public void SetAnimation(EState state)
    {
        switch(state)
        {
            case EState.Idle:
                anim.SetTrigger("idle");
                break;
            case EState.Walk:
                anim.SetTrigger("walk");
                break;
            case EState.Attack:
                anim.SetTrigger("attack");
                break;
        }
    }

    private IEnumerator SetHitColor()
    {
        spRen.color = new Color(1f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.2f);
        spRen.color = Color.white;
    }

    private void GetHitPush(Vector2 normal)
    {
        if (info.unitType == UnitType.Player)
            return;

        rb?.AddForce(normal * 500);
    }

    private bool IsInEnemyDis(Vector2 enemyPos)
    {
        return info.attackDistance > MathF.Abs(enemyPos.x - this.transform.position.x);
    }
}