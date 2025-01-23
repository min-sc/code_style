using UnityEngine;

public class GoldSkill : SkillBase
{
    private int buffAmount;

    public GoldSkill(int amount)
    {
        buffAmount = amount;
    }

    public void Apply(Character character, System.Action onApply = null)
    {
        character.AddGold(buffAmount);

        onApply?.Invoke();
    }
}