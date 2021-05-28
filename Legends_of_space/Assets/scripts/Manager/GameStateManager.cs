using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance; // 1

    [HideInInspector]
    public int enemySaved; // 2
    public int enemySavedBeforeGameOver; // 4

    [HideInInspector]
    public int enemyDropped; // 3

    public int enemyDroppedBeforeGameOver; // 4
    public EnemySpawner enemySpawner; // 5
                                      // Start is called before the first frame update
                                      // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void SavedSheep()
    {
        enemySaved++;
        if (enemySaved == enemySavedBeforeGameOver) // 2
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        enemySpawner.canSpawn = false; // 1
        enemySpawner.DestroyAllEnemy(); // 2
    }

    public void DroppedEnemy()
    {
        enemyDropped++; // 1

        if (enemyDropped == enemyDroppedBeforeGameOver) // 2
        {
            GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
