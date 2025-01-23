using UnityEngine;

public class Weapon_Arrow : Weapon
{
    private float waitTime = 0.3f;
    private float curTime = 0f;
    private float flySpeed = 40f;

    public override void InitWeapon(AttackInfo info)
    {
        base.InitWeapon(info);

        transform.rotation = Quaternion.Euler(0f, 0f, 20f);
        rb.AddForce(new Vector2(20f, 50f));
    }

    public override void Moving()
    {
        if (attackInfo.tr == null)
        {
            rb.AddForce(Vector2.down * flySpeed);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            return;
        }

        if (curTime < waitTime)
        {
            curTime += Time.deltaTime;
            return;
        }

        var dir = new Vector2(attackInfo.tr.position.x, attackInfo.tr.position.y) - new Vector2(transform.position.x, transform.position.y);
        var rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;// + 90f; 
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        rb.AddForce(dir.normalized * flySpeed);
    }
}
