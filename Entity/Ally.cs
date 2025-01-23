using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ally : Character
{
    public Ally(string name, int health, int attackPower, int defensePower, float attckSpeed, float attackDistance)
        : base(name, health, attackPower, defensePower, attckSpeed, attackDistance) { }

    [SerializeField] Vector3 centerOffset;
    public Func<float, Character> OnDetact { get; set; }
    float offsetMin, offsetMax;

    private float CurrentOffsetX
    {
        get { return transform.position.x - Player.Instance.GetPosition().x; }
    }

    public Direc InitPosFromMine
    {
        get 
        { 
            if(CurrentOffsetX < offsetMin)
                return Direc.Left;

            if(CurrentOffsetX > offsetMax)
                return Direc.Right;

            return Direc.Center;
        }
    }

    protected override void Start()
    {
        base.Start();

        info.state = CharacterState.Attack;

        OnDetact = (dist) =>
        {
            var nearEnemy = EnemyManager.Instance.GetNearEnemy();
            if (nearEnemy == null)
                return null;

            if (dist >= nearEnemy.GetPosition().x - GetPosition().x)
                return nearEnemy;

            return null;
        };

        offsetMin = CurrentOffsetX - 0.3f;
        offsetMax = CurrentOffsetX + 0.3f;
    }

#if UNITY_EDITOR
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(transform.position + centerOffset, new Vector3(9f, 9f, 0f));
    //}
#endif
}
