using UnityEngine;

[CreateAssetMenu(menuName = "Info/Character")]
public class CharacterInfo : ScriptableObject
{
    public string characterName;
    public int health;
    public int attackPower;
    public int defensePower;
    public float attackSpeed;
    public float attackDistance;
    public CharacterState state;
    public UnitType unitType;
    public float spawnOffset;
    public float moveSpeed;
}
