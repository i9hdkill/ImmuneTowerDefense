using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    [SerializeField]
    private EntityInWave[] enemies;
    [SerializeField]
    private float warmupTime;
    private float remainingWarmupTime;
    [SerializeField]
    private float timeBetweenSpawns;
    private int currentEnemy = 0;
    private int enemiesAlive;
    private bool isBossWave;
    private List<Enemy> tempEnemies = new List<Enemy>();

    public int RemainingWarmupTime {
        get {
            if (remainingWarmupTime > 0) {
                return Convert.ToInt32(remainingWarmupTime);
            } else {
                return 0;
            };
        }
    }

    public float WarmupTime {
        get {
            return warmupTime;
        }
    }

    public int EnemiesAlive {
        get {
            return enemiesAlive;
        }
    }

    public void StartWave() {
        StartCoroutine(Spawner());
    }

    private void Start() {
        enemiesAlive = enemies.Length;
        if (enemies.Length == 1) {
            isBossWave = true;
            ActionText.Instance.SetActionText(Manager.Instance.BosswaveSprite, 2f);
            Sound.Instance.PlayMusicClip(MusicEnum.BossLoop, true);
        }
        EventManager.Instance.OnEnemyDeathEvent += DecreaseEnemyCount;
        remainingWarmupTime = warmupTime;
    }

    private IEnumerator Spawner() {
        yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        SpawnNext();
    }

    private void Update() {
        if (remainingWarmupTime > 0) {
            remainingWarmupTime -= Time.deltaTime;
        }
    }

    private void DecreaseEnemyCount(Enemy entity, bool killedByPlayer) {
        if (tempEnemies.Contains(entity)) {
            enemiesAlive--;
            tempEnemies.Remove(entity);
            if (enemiesAlive == 0) {
                if (isBossWave) {
                    Sound.Instance.PlayMusicClip(MusicEnum.IngameLoop, true);
                }
                EventManager.Instance.WaveFinish();
            }
        }
    }

    private void SpawnNext() {
        if (Manager.Instance.IsPaused) {
            StartCoroutine(Spawner());
            Debug.Log("Waiting");
            return;
        }
        Enemy enemy = PoolHolder.Instance.GetObject(enemies[currentEnemy].EntityName).GetComponent<Enemy>();
        enemy.gameObject.transform.position = MapManager.Instance.GetTileByCoord(MapManager.Instance.StartCoord).transform.position;
        enemy.gameObject.SetActive(true);
        for (int i = 1; i < enemies[currentEnemy].EntityLevel; i++) {
            enemy.Upgrade();
        }
        enemy.SetPath(MapManager.Instance.WalkablePath);
        enemy.WaveNumber = Manager.Instance.CurrentWaveNumber;
        tempEnemies.Add(enemy);
        currentEnemy++;
        if (currentEnemy == enemies.Length) {
            return;
        }
        StartCoroutine(Spawner());
    }

}
