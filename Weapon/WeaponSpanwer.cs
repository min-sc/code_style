using System;

public class WeaponSpanwer : Pooling
{
    //float spawnTime = 0.5f;
    //int curDamage;

    //public Func<int> onShotStart;
    //public Action onShotEnd;

    protected override void Awake()
    {
        base.Awake();

        isWorldParent = true;
    }

    public void SpawnRange(AttackInfo info, Action onShotEnd = null)
    {
        //this.onShotEnd = onShotEnd;

        var obj = GetObject();
        obj.GetComponent<Weapon>().InitWeapon(info);

        onShotEnd?.Invoke();

        //StartCoroutine("ShotStart", info);
    }

    //public IEnumerator ShotStart(AttackInfo info)
    //{
    //    var obj = GetObject();
    //    obj.GetComponent<Weapon>().InitWeapon(info);

    //    yield return new WaitForSeconds(info.spawnTime);

    //    onShotEnd?.Invoke();
    //}

    //public void ShotStop()
    //{
    //    CancelInvoke("ShotStart");
    //}

    //private void OnDestroy()
    //{
    //    ShotStop();
    //}
}
