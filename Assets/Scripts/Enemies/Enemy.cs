using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : ParentObject {

    public int WaveNumber;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float currentHealthpoints;
    [SerializeField]
    private float healthpoints;
    [SerializeField]
    private float healthPerLevelUp;
    private Tower damagedFromTower;
    [SerializeField]
    private int moneyToAdd;
    private int defaultMoney = 0;
    [SerializeField]
    private int moreMoneyOnLevelUp;
    [SerializeField]
    private int scoreToAdd;
    private bool isSlowed;
    [SerializeField]
    private int playerLivesToRemove;
    [SerializeField]
    private EnemyTypeEnum enemyType;
    private EntityStateEnum entityState;
    [SerializeField]
    private Renderer ownRenderer;
    [SerializeField]
    private Image debuffImage;
    [SerializeField]
    private SoundDataHolder takeDamageSoundData;
    [SerializeField]
    private SoundDataHolder dieSoundData;
    [SerializeField]
    private SoundDataHolder idleSoundData;
    private DebuffTypeEnum currentDebuff;
    [SerializeField]
    private InfoBar infoBar;
    [SerializeField]
    private Sprite typeIcon;
    private Stack<Tile> waypoints;
    private Vector3 nextWorldPos;
    private float timer = 0;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float defaultHealthpoints;
    private float defaultSpeed;
    private float currentLevel = 1;
    private new Camera camera;

    public int ScoreToAdd {
        get {
            return scoreToAdd;
        }
    }

    public int MoneyToAdd {
        get {
            return moneyToAdd;
        }
    }

    public float Healthpoints {
        get {
            return healthpoints;
        }
    }

    public float CurrentHealthpoints {
        get {
            return currentHealthpoints;
        }
    }

    public Tower DamagedFromTower {
        set {
            damagedFromTower = value;
        }
    }

    public int PlayerLivesToRemove {
        get {
            return playerLivesToRemove;
        }
    }

    public Sprite TypeIcon {
        get {
            return typeIcon;
        }
    }

    public DebuffTypeEnum CurrentDebuff {
        get {
            return currentDebuff;
        }
    }

    public float CurrentLevel {
        get {
            return currentLevel;
        }
    }

    public bool IsAlive() {
        return entityState == EntityStateEnum.Alive;
    }

    public void SetPath(Stack<Tile> wayPoints) {
        waypoints = wayPoints;
        if (waypoints != null) {
            nextWorldPos = wayPoints.Pop().PositionInWorld;
        } else {
            anim.enabled = false;
        }
    }

    public void Upgrade() {
        currentLevel++;
        moneyToAdd += moreMoneyOnLevelUp;
        healthpoints += healthPerLevelUp;
        currentHealthpoints = healthpoints;
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        infoBar.Show();
        infoBar.UpdateBar();
    }

    public void TakeDamage(float damage) {
        if (!infoBar.IsEnabled()) {
            infoBar.Show();
        }
        if (currentHealthpoints - damage < 0) {
            currentHealthpoints = 0;
        } else {
            currentHealthpoints -= damage;
            StartCoroutine(FlashRed());
        }
        if (currentHealthpoints <= 0) {
            Die(true);
        }
        infoBar.UpdateBar();
    }

    public void ApplyDebuff(Sprite debuffSprite, float duration, DebuffTypeEnum type, float slowAmount, float slowTime) {
        debuffImage.gameObject.SetActive(true);
        debuffImage.enabled = true;
        debuffImage.sprite = debuffSprite;
        debuffImage.preserveAspect = true;
        currentDebuff = type;
        Slow(slowAmount, slowTime);
    }

    protected override void OnEnable() {
        base.OnEnable();
        Reset();
        EventManager.Instance.OnDeselectEvent += infoBar.Hide;
        infoBar.UpdateBar();
        infoBar.Hide();
        Sound.Instance.PlaySoundClipWithSource(idleSoundData.SoundName, audioSource, 0, idleSoundData.Volume);
    }

    protected override void Start() {
        base.Start();
        camera = Camera.main;
        defaultSpeed = moveSpeed;
        defaultMoney = moneyToAdd;
    }

    private void Reset() {
        currentDebuff = DebuffTypeEnum.None;
        debuffImage.enabled = false;
        if (defaultMoney != 0) {
            moneyToAdd = defaultMoney;
        }
        currentLevel = 1;
        healthpoints = defaultHealthpoints;
        currentHealthpoints = healthpoints;
        entityState = EntityStateEnum.Alive;
        ownRenderer.material.color = Color.white;
        damagedFromTower = null;
        anim.enabled = true;
    }

    private void Update() {
        if (isInShowMode || Manager.Instance.IsPaused) {
            return;
        }
        debuffImage.transform.position = camera.WorldToScreenPoint(transform.position);
        Move();
        if (timer > 10f && IsAlive()) {
            timer = 0;
            int rnd = Random.Range(0, 5);
            if (rnd == 1) {
                Sound.Instance.PlaySoundClipWithSource(idleSoundData.SoundName, audioSource, 0, idleSoundData.Volume);
            }
        }
        timer += Time.deltaTime;
    }

    private void Slow(float percentage, float duration) {
        if (!isSlowed) {
            StartCoroutine(ApplySlow(percentage, duration));
        }
    }

    private IEnumerator ApplySlow(float percentage, float duration) {
        isSlowed = true;
        moveSpeed *= percentage;
        yield return new WaitForSeconds(duration);
        debuffImage.enabled = false;
        currentDebuff = DebuffTypeEnum.None;
        moveSpeed = defaultSpeed;
        isSlowed = false;
    }

    private void Move() {
        if (waypoints != null) {
            transform.position = Vector3.MoveTowards(transform.position, nextWorldPos, moveSpeed * Time.deltaTime * Manager.Instance.SpeedMultiplier);
            if (transform.position == nextWorldPos) {
                if (waypoints.Count > 0) {
                    nextWorldPos = waypoints.Pop().PositionInWorld;
                    transform.LookAt(nextWorldPos);
                } else {
                    Die(false);
                }
            }
        }
    }

    private IEnumerator FlashRed() {
        ownRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        ownRenderer.material.color = Color.white;
    }

    private void Die(bool fromPlayer) {
        entityState = EntityStateEnum.Dead;
        EventManager.Instance.EnemyDeath(this, fromPlayer);
        if (damagedFromTower != null) {
            damagedFromTower.RemoveEnemyFromList(this);
        }
        if (fromPlayer) {
            PoolHolder.Instance.GetObject(ParentObjectNameEnum.DeathObject).GetComponent<DeathObject>().Activate(dieSoundData, transform.position);
        }
        infoBar.UpdateBar();
        infoBar.Hide();
        moveSpeed = defaultSpeed;
        gameObject.SetActive(false);
    }

    protected override void OnDisable() { }

}
