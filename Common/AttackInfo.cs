using UnityEngine;

public class AttackInfo
{
    public AttackInfo(int damage, float maxDistance, float time, Transform tr)
    {
        this.damage = damage;
        this.maxDistance = maxDistance;
        this.spawnTime = time;
        this.tr = tr;
    }

    public int damage;
    public float maxDistance;
    public float spawnTime;
    public Transform tr;
}
