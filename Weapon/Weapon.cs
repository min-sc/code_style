using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    const string enemyTag = "enemy";
    const string playerTag = "player";
    const string groundTag = "ground";

    public GameObject effect;
    public UnitType FollowType;
    public int Damage { get { return attackInfo.damage; } }

    protected Rigidbody2D rb;
    protected AttackInfo attackInfo = null;

    private float initPosx = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initPosx = this.transform.position.x;
    }

    public virtual void InitWeapon(AttackInfo info)
    {
        attackInfo = info;
        attackInfo.maxDistance += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == groundTag)
        {
            Destroy(this.gameObject);
            return;
        }

        if (IsEnemy(tag))
        {
            Instantiate(effect, collision.GetContact(0).point, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (Mathf.Abs(initPosx - this.transform.position.x) > attackInfo.maxDistance)
        {
            Destroy(this.gameObject);
            return;
        }

        Moving();
    }

    bool IsEnemy(string tag)
    {
        return FollowType == UnitType.Enemy && tag == enemyTag || FollowType == UnitType.Player && tag == playerTag;
    }

    public abstract void Moving();
}
