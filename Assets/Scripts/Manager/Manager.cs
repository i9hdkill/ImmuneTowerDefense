using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager Instance { get; private set; }

    public int CurrentWaveNumber = 0;
    public GameObject BuildingPrefabOnMouse;
    public int PlayerScore;

    [SerializeField]
    private float fastForwardSpeed;
    private float speedMultiplier = 1;
    [SerializeField]
    private GameObject beforeGameScreen;
    [SerializeField]
    private IngameMenu ingameMenu;
    [SerializeField]
    private TopBar topBar;
    [SerializeField]
    private Cheats cheats;
    [SerializeField]
    private Heart heart;
    [SerializeField]
    private GameOverScreen gameOverScreen;
    [SerializeField]
    private WinScreen winScreen;
    [SerializeField]
    private GameObject game;
    private bool isPaused;
    private bool isStarted;
    [SerializeField]
    private Sprite moneySprite;
    [SerializeField]
    private Sprite damageSprite;
    [SerializeField]
    private Sprite bosswaveSprite;
    [SerializeField]
    private Sprite newWaveSprite;
    [SerializeField]
    private int playerLifes;
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Options options;

    public int PlayerMoney { get; private set; } = 250;

    public MainMenu MainMenu {
        get {
            return mainMenu;
        }
    }

    public GameObject BeforeGameScreen {
        get {
            return beforeGameScreen;
        }
    }

    public Options Options {
        get {
            return options;
        }
    }

    public bool IsPaused {
        get {
            return isPaused;
        }
    }

    public bool IsStarted {
        get {
            return isStarted;
        }
    }

    public int MaxWaveNumber { get; internal set; } = 0;

    public IngameMenu IngameMenu {
        get {
            return ingameMenu;
        }
    }

    public Sprite MoneySprite {
        get {
            return moneySprite;
        }
    }

    public TopBar TopBar {
        get {
            return topBar;
        }
    }

    public Heart Heart {

        set {
            heart = value;
        }
    }

    public Sprite NewWaveSprite {
        get {
            return newWaveSprite;
        }
    }

    public Sprite BosswaveSprite {
        get {
            return bosswaveSprite;
        }
    }

    public int PlayerLifes {
        get {
            return playerLifes;
        }
    }

    public float SpeedMultiplier {
        get {
            return speedMultiplier;
        }
    }

    public void IncreaseSpeed() {
        speedMultiplier = fastForwardSpeed;
    }

    public void ReduceSpeed() {
        speedMultiplier = 1;
    }

    public void SubtractLifes(int lifesToRemove) {
        playerLifes -= lifesToRemove;
        Sound.Instance.PlaySoundClip(SoundEnum.Props_Heart_Damage, 1f);
        if (playerLifes <= 0) {
            EventManager.Instance.GameOver();
        }
    }

    public void AddMoney(int moneyToAdd) {
        PlayerMoney += moneyToAdd;
        EventManager.Instance.RefreshUI();
    }

    public void SubtractMoney(int moneyToRemove) {
        PlayerMoney -= moneyToRemove;
        EventManager.Instance.RefreshUI();
    }

    public bool HasEnoughMoney(int money) {
        return PlayerMoney >= money;
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        game.SetActive(false);
        beforeGameScreen.SetActive(true);
    }

    private void Start() {
        EventManager.Instance.OnBuildingBuiltEvent += BuildingBuiltActions;
        EventManager.Instance.OnBuildingRemovedEvent += BuildingRemovedActions;
        EventManager.Instance.OnEnemyDeathEvent += EntityDeathActions;
        EventManager.Instance.OnGameWinEvent += GameWin;
        EventManager.Instance.OnQuitGameEvent += QuitGame;
        EventManager.Instance.OnGameOverEvent += GameOver;
        EventManager.Instance.OnStartPauseEvent += StartPause;
        EventManager.Instance.OnEndPauseEvent += EndPause;
        EventManager.Instance.OnStartGamePostEvent += SetGameStart;
        EventManager.Instance.OnGameResetEvent += ResetGame;
        Sound.Instance.PlayMusicClip(MusicEnum.MenuLoop, true);
    }

    private void EntityDeathActions(Enemy deadEntity, bool killedByPlayer) {
        if (killedByPlayer) {
            PlayerScore += deadEntity.ScoreToAdd;
            AddMoney(deadEntity.MoneyToAdd);
            return;
        }
        heart.PlayParticles();
        SubtractLifes(deadEntity.PlayerLivesToRemove);
    }

    private void BuildingBuiltActions(Tower building) { }

    private void BuildingRemovedActions(Tower building) { }

    private void ResetGame() {
        CurrentWaveNumber = 0;
        PlayerMoney = 250;
        PlayerScore = 0;
        playerLifes = 100;
        EventManager.Instance.RefreshUI();
        EventManager.Instance.EndPause();
    }

    private void QuitGame() {
        isStarted = false;
    }

    private void GameOver() {
        gameOverScreen.Show();
        topBar.Hide();
        Sound.Instance.PlayMusicClip(MusicEnum.GameOverLoop, true);
        EventManager.Instance.StartPause();
    }

    private void StartPause() {
        isPaused = true;
    }

    private void EndPause() {
        isPaused = false;
    }

    private void SetGameStart() {
        isStarted = true;
        Sound.Instance.PlayMusicClip(MusicEnum.IngameLoop, true);
        game.SetActive(true);
    }

    private void GameWin() {
        Sound.Instance.PlaySoundClip(SoundEnum.Enemy_Malaria_Death, 1);
        winScreen.Show();
        topBar.Hide();
        EventManager.Instance.StartPause();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C) && !cheats.isActiveAndEnabled) {
            cheats.Show();
            return;
        }
        if (Input.GetKeyDown(KeyCode.C) && cheats.isActiveAndEnabled) {
            cheats.Hide();
        }
        if (!isStarted || BuildingPrefabOnMouse != null) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !ingameMenu.isActiveAndEnabled) {
            ingameMenu.Show();
            EventManager.Instance.StartPause();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && ingameMenu.isActiveAndEnabled) {
            ingameMenu.Resume();
            EventManager.Instance.EndPause();
        }
    }

}
