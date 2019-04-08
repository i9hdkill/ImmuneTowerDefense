using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Menu {

    [SerializeField]
    private Text waveNumbers;
    [SerializeField]
    private Text currentMoney;
    [SerializeField]
    private Text currentLifes;
    [SerializeField]
    private Text currentScore;
    [SerializeField]
    private Text remainingWaveEnemies;
    private float timer = 0;

    private void Start() {
        EventManager.Instance.OnStartGamePostEvent += SetStartValues;
        EventManager.Instance.OnWaveStartEvent += AdjustWaveCount;
        EventManager.Instance.OnWaveFinishEvent += SetWaveNumbers;
        EventManager.Instance.OnWaveFinishEvent += SetCurrentScoreNumber;
        EventManager.Instance.OnEnemyDeathEvent += SetCurrentMoneyNumber;
        EventManager.Instance.OnEnemyDeathEvent += SetCurrentLifeNumber;
        EventManager.Instance.OnEnemyDeathEvent += SetCurrentScoreNumber;
        EventManager.Instance.OnRefreshUIEvent += SetCurrentMoneyNumber;
    }

    private void SetCurrentScoreNumber(Enemy deadEntity, bool killedByPlayer) {
        if (killedByPlayer) {
            SetCurrentScoreNumber();
        }
    }

    private void Update() {
        if (WaveManager.Instance.CurrentWave != null && timer > 0.3f) {
            remainingWaveEnemies.text = WaveManager.Instance.CurrentWave.EnemiesAlive.ToString();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void AdjustWaveCount() {
        if (Manager.Instance.CurrentWaveNumber <= Manager.Instance.MaxWaveNumber) {
            waveNumbers.text = Manager.Instance.CurrentWaveNumber.ToString() + Path.AltDirectorySeparatorChar + Manager.Instance.MaxWaveNumber;
        }
    }

    private void SetCurrentScoreNumber() {
        currentScore.text = Manager.Instance.PlayerScore.ToString();
    }

    private void SetStartValues() {
        SetWaveNumbers();
        SetCurrentLifeNumber();
        SetCurrentMoneyNumber();
        EventManager.Instance.Deselect();
        EventManager.Instance.RefreshUI();
    }

    private void SetWaveNumbers() {
        int waveNumber = Manager.Instance.CurrentWaveNumber;
        if (waveNumber > Manager.Instance.MaxWaveNumber) {
            waveNumber = Manager.Instance.MaxWaveNumber;
        }
        waveNumbers.text = waveNumber.ToString() + Path.AltDirectorySeparatorChar + Manager.Instance.MaxWaveNumber;
    }

    private void SetCurrentLifeNumber(ParentObject pObject, bool killedByPlayer) {
        if (!killedByPlayer) {
            SetCurrentLifeNumber();
        }
    }

    private void SetCurrentLifeNumber() {
        currentLifes.text = Manager.Instance.PlayerLifes.ToString();
    }

    private void SetCurrentMoneyNumber(ParentObject pObject, bool killedByPlayer) {
        if (killedByPlayer) {
            SetCurrentMoneyNumber();
        }
    }

    private void SetCurrentMoneyNumber() {
        currentMoney.text = Manager.Instance.PlayerMoney.ToString();
    }

}
