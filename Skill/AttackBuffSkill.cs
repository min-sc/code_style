using UnityEngine;

public class AttackBuffSkill : SkillBase
{
    private int buffAmount;

    public AttackBuffSkill(int amount)
    {
        buffAmount = amount;
    }

    public void Apply(Character character, System.Action onApply = null)
    {
        GameObject obj = Object.Instantiate<GameObject>(Resources.Load("Attack") as GameObject, character.HealSpawn);
        obj.transform.position = character.HealSpawn.position;

        AutoDeath eventer = obj.GetComponent<AutoDeath>();
        eventer.OnEvent = () => { character.AddStrong(buffAmount); };

        onApply?.Invoke();
    }
}