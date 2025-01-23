using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper
{

}

public enum CharacterState
{
    Idle,
    Walk,
    ForceWalk,
    Attack,
    Die,
    None
}

public enum EState
{
    Idle, Walk, Attack
}

public enum Direction
{
    up,
    down,
    right,
    left
}

public enum UnitType
{
    Player,
    Enemy
}

public enum Direc
{
    Right,
    Left,
    Center,
    Up,
    Down
}

public enum SkillType
{
    Buff,
    Gold,
    Attack
}

public enum Skill
{
    Bomb,
    Electric,
    Heal,
    Defend,
    Gold,
    AttackBuff
}