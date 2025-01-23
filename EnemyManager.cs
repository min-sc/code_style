using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] LevelInfo level;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject tempEnemy;

    public Func<float, Character> OnDetact { get; set; }
    public Func<Enemy, bool> OnDeath { get; set; }
    private List<Enemy> enemies = new List<Enemy>();

    private int curTurnCount = 0;
    private int curTurnEnemyCount = 0;

    protected override void Awake()
    {
        base.Awake();

        OnDetact = (dist) =>
        {
            var enemy = GetNearEnemy();

            if (enemy != null && dist >= GetNearEnemy().GetPosition().x - Player.Instance.GetPosition().x)
                return GetNearEnemy();

            return null;
        };

        OnDeath = (ene) =>
        {
            enemies.Remove(ene);

            return true;
        };

        curTurnCount = 0;
        curTurnEnemyCount = 0;
    }

    public void TurnStart()
    {
        StartCoroutine("SpawnEnemies");
    }

    public void TurnEnd()
    {
        StopAllCoroutines();
        //StartCoroutine("SpawnEnd");
    }

    IEnumerator SpawnEnd()
    {
        curTurnCount++;

        yield return new WaitForSeconds(2f);
    }

    IEnumerator SpawnEnemies()
    {
        if (spawnPoint == null) 
            yield break;

        GameObject enemy = Instantiate<GameObject>(GetRandomEnemy(), this.transform);
        Enemy em = enemy.GetComponent<Enemy>();
        enemies.Add(em);
        enemy.transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y + em.info.spawnOffset);

        yield return new WaitForSeconds(UnityEngine.Random.Range(level.minSpawnTime, level.maxSpawnTime));

        curTurnEnemyCount++;

        if (IsEndCurTurn())
        {
            if (IsEndAllTurn())
                StartCoroutine("SpawnEnd");
            else
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(level.minSpawnTime, level.maxSpawnTime));
                TurnStart();
            }
        }
        else
            StartCoroutine("SpawnEnemies");
    }

    GameObject GetRandomEnemy()
    {
        float pickValue = UnityEngine.Random.Range(0f, level.spawnRange.Sum());
        float curValue = 0f;
        int count = 0;
        foreach (var rag in level.spawnRange)
        {
            curValue += rag;

            if (pickValue < curValue)
                return level.enemies[count];

            count++;
        }

        return null;
    }

    bool IsEndCurTurn()
    {
        return curTurnEnemyCount >= level.enemiesByTurn;
    }

    bool IsEndAllTurn()
    {
        return curTurnCount >= level.turn;
    }

    public Enemy GetNearEnemy()
    {
        if (enemies.Count == 0)
            return null;

        return enemies.OrderBy(x => x.transform.position.x).First();
    }

    public Enemy[] GetAllEnemy()
    {
        if (enemies.Count == 0)
            return null;

        return enemies.ToArray();
    }
}
