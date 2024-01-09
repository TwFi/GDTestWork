using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public EntityPrefubs Entities { get; private set; }
    [SerializeField] private LevelConfig Config;
    [SerializeField] public List<Enemie> Enemies;

    public event Action OnWin;
    public event Action OnLose;
    public event Action<int> OnWaveSpawned;
    public int MaxWaves => Config.Waves.Length;

    private int currentWave = 0;
    
    private void Start()
    {
        SpawnWave();
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
        enemie.OnDie += RemoveEntity;
    }

    public void RemoveEntity(Entity entity)
    {
        if (entity is Enemie enemie)
            RemoveEnemie(enemie);
    }

    private void RemoveEnemie(Enemie enemie)
    {
        Enemies.Remove(enemie);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void GameOver()
    {
        OnLose?.Invoke();
    }

    private void SpawnWave()
    {
        if (currentWave >= Config.Waves.Length)
        {
            OnWin?.Invoke();
            return;
        }

        var wave = Config.Waves[currentWave];
        foreach (var character in wave.Characters)
        {
            EnemyCreator.Instance.CreateOnRandomPosition(character);
        }

        currentWave++;
        OnWaveSpawned?.Invoke(currentWave);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
