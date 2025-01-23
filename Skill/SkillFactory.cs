using UnityEngine;

public class SkillFactory : MonoBehaviour
{
    [SerializeField]
    Transform skillParent;

    [SerializeField]
    GameObject[] skills;

    public SkillBase CreateSkill(Skill sk) 
    { 
        switch(sk)
        {
            case Skill.Bomb:
                return new BombSkill();
            case Skill.Electric:
                return new ElectricSkill();
            case Skill.Heal:
                return new HealSkill(5);//temp
            case Skill.Defend:
                return new DefendSkill(2);//temp
            case Skill.Gold:
                return new GoldSkill(500);//temp
            case Skill.AttackBuff:
                return new AttackBuffSkill(5);//temp
        }
        
        Debug.LogWarning("Not matched skill pz check");
        return null;
    }
}
