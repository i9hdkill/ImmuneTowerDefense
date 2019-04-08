using cakeslice;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : ParentObject {

    [SerializeField]
    private int currentTowerLevel;
    [SerializeField]
    private int maxTowerLevel;
    [SerializeField]
    private int buildCost;
    [SerializeField]
    private int upgradeCost;
    [SerializeField]
    private int upgradeCostIncreasePerLevel;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int enemyAttacksAtTheSameTime;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private ParticleSystem shootParticles;
    [SerializeField]
    private float shootVolume;
    [SerializeField]
    private SoundDataHolder finishBuildSoundData;
    [SerializeField]
    private SoundDataHolder levelUpSoundData;
    [SerializeField]
    private SoundDataHolder destroySoundData;
    [SerializeField]
    private SoundDataHolder shootSoundData;
    private float timer = 10;
    private List<Enemy> enemiesInRange = new List<Enemy>();
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private TowerBehaviour towerBehaviour;
    [SerializeField]
    private GameObject projectileRoot;
    private float oldProjectileDamage;

    public int UpgradeCost {
        get {
            return upgradeCost;
        }
    }

    public int MaxTowerLevel {
        get {
            return maxTowerLevel;
        }
    }

    public int BuildCost {
        get {
            return buildCost;
        }
    }

    public GameObject Projectile {
        get {
            return projectile;
        }
    }

    public int CurrentTowerLevel {
        get {
            return currentTowerLevel;
        }
    }

    public SoundDataHolder ShootSoundData {
        get {
            return shootSoundData;
        }
    }

    public GameObject ProjectileRoot {
        get {
            return projectileRoot;
        }
    }

    public List<Enemy> EnemiesInRange {
        get {
            return enemiesInRange;
        }
    }

    public bool Upgrade() {
        bool canBeUpgraded = false;
        if (currentTowerLevel >= maxTowerLevel) {
            return canBeUpgraded;
        } else if (!Manager.Instance.HasEnoughMoney(UpgradeCost)) {
            return canBeUpgraded;
        }
        Manager.Instance.SubtractMoney(UpgradeCost);
        Sound.Instance.PlaySoundClipWithSource(levelUpSoundData.SoundName, audioSource, 0, levelUpSoundData.Volume);
        currentTowerLevel++;
        gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(1.1f, 1.1f, 1.1f));
        upgradeCost += upgradeCostIncreasePerLevel;
        projectile.GetComponent<Projectile>().Upgrade();
        EventManager.Instance.RefreshUI();
        return !canBeUpgraded;
    }

    public void Destroy() {
        EventManager.Instance.BuildingRemoved(this);
        Manager.Instance.AddMoney(buildCost / 2);
        gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
    }

    public void RemoveEnemyFromList(Enemy enemyToRemove) {
        if (enemiesInRange.Contains(enemyToRemove)) {
            enemiesInRange.Remove(enemyToRemove);
        }
    }

    protected override void Start() {
        base.Start();
        oldProjectileDamage = projectile.GetComponent<Projectile>().ProjectileDamage;
    }

    protected override void OnEnable() {
        base.OnEnable();
        enemiesInRange = new List<Enemy>();
        audioSource.volume = Sound.Instance.SoundVolume;
        gameObject.GetComponent<SphereCollider>().radius = attackRange;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        Sound.Instance.PlaySoundClipWithSource(finishBuildSoundData.SoundName, audioSource, 0, finishBuildSoundData.Volume);
        Reset();
    }

    private void Reset() {
        currentTowerLevel = 1;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter(Collider other) {
        Enemy temp = other.gameObject.GetComponent<Enemy>();
        if (temp != null && temp.IsAlive()) {
            enemiesInRange.Add(temp);
        }
    }

    private void OnTriggerExit(Collider other) {
        RemoveEnemyFromList(other.gameObject.GetComponent<Enemy>());
    }

    private void Update() {
        if (Manager.Instance.IsPaused || isInShowMode) {
            return;
        }
        if (enemiesInRange.Count > 0 && timer >= attackCooldown) {
            for (int i = 0; i < enemyAttacksAtTheSameTime; i++) {
                if (enemiesInRange.Count >= i + 1 ) {
                    towerBehaviour.Attack(enemiesInRange[i]);
                    timer = 0;
                }
            }
            for (int i = 0; i < enemiesInRange.Count; i++) {
                if (enemiesInRange[i] != null && !enemiesInRange[i].IsAlive()) {
                    RemoveEnemyFromList(enemiesInRange[i]);
                }
            }
        }
        timer += Time.deltaTime;
        towerBehaviour.Rotate();
    }

    protected override void OnDisable() {
        base.OnDisable();
        projectile.GetComponent<Projectile>().ResetProjectile(oldProjectileDamage);
    }

}
