using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Magicball : Weapon
{
    private float flySpeed = 10f;
    private float rotSpeed = 30f;
    private Vector2 dir;

    public override void InitWeapon(AttackInfo info)
    {
        base.InitWeapon(info);

        dir = FollowType == UnitType.Player ? Vector2.left : Vector2.right;
        rotSpeed = FollowType == UnitType.Player ? rotSpeed : rotSpeed * -1f;
        dir = new Vector2(attackInfo.tr.position.x, attackInfo.tr.position.y) - new Vector2(transform.position.x, transform.position.y);
    }

    public override void Moving()
    {
        rb.AddForce(dir.normalized * flySpeed);
        transform.Rotate(new Vector3(0f, 0f, rotSpeed));
    }
}
