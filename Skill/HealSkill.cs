using UnityEngine;

public class HealSkill : SkillBase
{
    private int buffAmount; 
    public HealSkill(int amount) 
    { 
        buffAmount = amount;
    }

    public void Apply(Character character, System.Action onApply = null)
    {
        GameObject obj = Object.Instantiate<GameObject>(Resources.Load("Heal") as GameObject, character.HealSpawn);
        obj.transform.position = character.HealSpawn.position;

        AutoDeath eventer = obj.GetComponent<AutoDeath>();
        eventer.OnEvent = () => { character.Heal(buffAmount); };

        onApply?.Invoke();
    }
}
