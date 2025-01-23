using UnityEngine;

public class ElectricSkill : SkillBase
{
    public ElectricSkill()
    {

    }

    public void Apply(Character character, System.Action onApply = null)
    {
        GameObject obj = Object.Instantiate<GameObject>(Resources.Load("Electric") as GameObject, character.transform.parent);
        obj.transform.position = character.SkillSpawnPos;

        AutoDeath eventer = obj.GetComponent<AutoDeath>();
        eventer.OnEvent = () =>
        {
            var enemies = EnemyManager.Instance.GetAllEnemy();

            if (enemies is null)
                return;

            foreach (var ene in enemies)
                ene.Defend(30);
        };

        onApply?.Invoke();
    }
}