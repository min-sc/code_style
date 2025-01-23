using System;

public class WeaponSpanwer : Pooling
{
    protected override void Awake()
    {
        base.Awake();

        isWorldParent = true;
    }

    public void SpawnRange(AttackInfo info, Action onShotEnd = null)
    {
        var obj = GetObject();
        obj.GetComponent<Weapon>().InitWeapon(info);

        onShotEnd?.Invoke();
    }
}
