using UnityEngine;

public class DefendSkill : SkillBase
{
    private int buffAmount;

    public DefendSkill(int amount)
    {
        buffAmount = amount;
    }

    public void Apply(Character character, System.Action onApply = null)
    {
        GameObject obj = Object.Instantiate<GameObject>(Resources.Load("Defend") as GameObject, character.HealSpawn);
        obj.transform.position = character.HealSpawn.position;

        AutoDeath eventer = obj.GetComponent<AutoDeath>();
        eventer.OnEvent = () =>
        {
            character.AddDefend(buffAmount);

        };

        onApply?.Invoke();
    }
}