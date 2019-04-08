
using UnityEngine;

public class EventManager {

    public static EventManager Instance { get { if (instance == null) { instance = new EventManager(); } return instance; } }

    private static EventManager instance;

    public delegate void DelSimple();
    public delegate void DelBool(bool value);
    public delegate void DelEnemy(Enemy deadEntity, bool killedByPlayer);
    public delegate void DelParentObject(ParentObject parentObject);
    public delegate void DelParentBuilding(Tower building);

    public event DelEnemy OnEnemyDeathEvent;
    public event DelParentObject OnParentObjectSelectedEvent;
    public event DelSimple OnDeselectEvent;
    public event DelEnemy OnEntityDamageEvent;
    public event DelEnemy OnEntitySpawnEvent;
    public event DelParentBuilding OnBuildingBuiltEvent;
    public event DelParentBuilding OnBuildingRemovedEvent;
    public event DelSimple OnWaveFinishEvent;
    public event DelSimple OnWaveStartEvent;
    public event DelSimple OnGameWinEvent;
    public event DelSimple OnRefreshUIEvent;
    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameOverPreEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameOverEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameResetPostEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameResetPreEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameResetEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler verloren hat
    /// </summary>
    public event DelSimple OnGameOverPostEvent;

    /// <summary>
    /// Event wird aufgerufen, bevor das Spiel gestartet wird
    /// </summary>
    public event DelSimple OnStartGamePreEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn das Spiel gestartet wird
    /// </summary>
    public event DelSimple OnStartGameEvent;

    /// <summary>
    /// Event wird aufgerufen, nachdem das Spiel gestartet wurde
    /// </summary>
    public event DelSimple OnStartGamePostEvent;

    /// <summary>
    /// Event wird aufgerufen, nachdem das Spiel pausiert wurde
    /// </summary>
    public event DelSimple OnStartPauseEvent;

    public event DelSimple OnEndPauseEvent;

    /// <summary>
    /// Event wird aufgerufen, nachdem das Spiel weitergeführt wurde
    /// </summary>
    public event DelSimple OnResumeGameEvent;

    /// <summary>
    /// Event wird aufgerufen, nachdem das Spiel gewonnen wurde
    /// </summary>
    public event DelSimple OnWinGameEvent;

    /// <summary>
    /// Event wird aufgerufen, wenn der Spieler das Spiel verlässt
    /// </summary>
    public event DelSimple OnQuitGameEvent;

    private void ParentObjectSelectedEvent(ParentObject selectedObject) {
        OnParentObjectSelectedEvent?.Invoke(selectedObject);
    }

    public void ParentObjectSelected(ParentObject selectedObject) {
        OnParentObjectSelectedEvent(selectedObject);
    }

    private void DeselectEvent() {
        OnDeselectEvent?.Invoke();
    }

    public void Deselect() {
        OnDeselectEvent();
    }

    private void EntityDamageEvent(Enemy damagedEntity, bool damagedByPlayer) {
        OnEntityDamageEvent?.Invoke(damagedEntity, damagedByPlayer);
    }

    public void EntityDamage(Enemy damagedEntity, bool damagedByPlayer) {
        OnEntityDamageEvent(damagedEntity, damagedByPlayer);
    }

    private void EntitySpawnEvent(Enemy spawnedEntity, bool spawnedByPlayer) {
        Debug.Log(spawnedEntity.name + " spawned");
        OnEntitySpawnEvent?.Invoke(spawnedEntity, spawnedByPlayer);
    }

    public void EntitySpawn(Enemy spawnedEntity, bool spawnedByPlayer) {
        OnEntitySpawnEvent(spawnedEntity, spawnedByPlayer);
    }

    private void EnemyDeathEvent(Enemy deadEntity, bool killedByPlayer) {
        OnEnemyDeathEvent?.Invoke(deadEntity, killedByPlayer);
    }

    public void EnemyDeath(Enemy deadEntity, bool killedByPlayer) {
        OnEnemyDeathEvent(deadEntity, killedByPlayer);
    }

    private void BuildingBuiltEvent(Tower building) {
        OnBuildingBuiltEvent?.Invoke(building);
    }

    public void BuildingBuilt(Tower building) {
        OnBuildingBuiltEvent(building);
    }

    private void BuildingRemovedEvent(Tower building) {
        OnBuildingRemovedEvent?.Invoke(building);
    }

    public void BuildingRemoved(Tower building) {
        OnBuildingRemovedEvent(building);
    }

    /// <summary>
    /// Methode die vor dem ausführen des GameOverEvent prüft ob sich Methoden auf diese Events angemeldet haben.
    /// </summary>
    private void GameOverEventChain() {
        OnGameOverPreEvent?.Invoke();
        OnGameOverEvent?.Invoke();
        OnGameOverPostEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der Spieler verloren hat.
    /// </summary>
    public void GameOver() {
        GameOverEventChain();
    }

    /// <summary>
    /// Methode die vor dem ausführen des GameResetEvent prüft ob sich Methoden auf diese Events angemeldet haben.
    /// </summary>
    private void GameResetEventChain() {
        OnGameResetPreEvent?.Invoke();
        OnGameResetEvent?.Invoke();
        OnGameResetPostEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der Spieler verloren hat.
    /// </summary>
    public void GameReset() {
        GameResetEventChain();
    }

    public void WaveFinish() {
        WaveFinishEvent();
    }

    private void WaveFinishEvent() {
        OnWaveFinishEvent?.Invoke();
    }

    private void GameWinEvent() {
        OnGameWinEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der Spieler gewonnen hat.
    /// </summary>
    public void GameWin() {
        GameWinEvent();
    }

    public void WaveStart() {
        WaveStartEvent();
    }

    private void WaveStartEvent() {
        OnWaveStartEvent?.Invoke();
    }

    private void RefreshUIEvent() {
        OnRefreshUIEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der Spieler gewonnen hat.
    /// </summary>
    public void RefreshUI() {
        RefreshUIEvent();
    }

    /// <summary>
    /// Methode die vor dem ausführen des StartGameEvent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void StartGameEventChain() {
        OnStartGamePreEvent?.Invoke();
        OnStartGameEvent?.Invoke();
        OnStartGamePostEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn das Spiel gestartet wirdt.
    /// </summary>
    public void StartGame() {
        StartGameEventChain();
    }

    /// <summary>
    /// Methode die vor dem ausführen des StartGameEvent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void StartPauseEvent() {
        OnStartPauseEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn das Spiel pausiert wird.
    /// </summary>
    public void StartPause() {
        StartPauseEvent();
    }

    /// <summary>
    /// Methode die vor dem ausführen des EndGameEvent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void EndPauseEvent() {
        OnEndPauseEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn das Spiel resumed wird.
    /// </summary>
    public void EndPause() {
        EndPauseEvent();
    }

    /// <summary>
    /// Methode die vor dem ausführen des ResumeGameEvent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void ResumeGameEvent() {
        OnResumeGameEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn das Spiel weitergeführt wird.
    /// </summary>
    public void ResumeGame() {
        ResumeGameEvent();
    }

    /// <summary>
    /// Methode die vor dem ausführen des WinGamevent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void WinGameEvent() {
        OnWinGameEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn das Spiel gewonnen wurde.
    /// </summary>
    public void WinGame() {
        WinGameEvent();
    }

    /// <summary>
    /// Methode die vor dem ausführen des QuitGameEvent prüft ob sich Methoden auf diesen Events angemeldet haben.
    /// </summary>
    private void QuitGameEvent() {
        OnQuitGameEvent?.Invoke();
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der Spieler das Spiel verlassen hat.
    /// </summary>
    public void QuitGame() {
        QuitGameEvent();
    }
}
