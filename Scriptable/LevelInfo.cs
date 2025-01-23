using UnityEngine;

[CreateAssetMenu(menuName = "Info/Level")]
public class LevelInfo : ScriptableObject
{
    public int level;
    public int turn;
    public int enemiesByTurn;
    public int enemiesLevel;
    public float minSpawnTime;
    public float maxSpawnTime;
    public GameObject[] enemies;
    public float[] spawnRange;
}
