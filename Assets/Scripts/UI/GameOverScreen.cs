using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : Menu {

    [SerializeField]
    private Text endScore;
    [SerializeField]
    private Text waveCount;

    public void BackToMainMenu() {
        Hide();
        Manager.Instance.TopBar.Show();
        Sound.Instance.PlayMusicClip(MusicEnum.MenuLoop, true);
        Manager.Instance.MainMenu.Show();
        Manager.Instance.BeforeGameScreen.SetActive(true);
        EventManager.Instance.GameReset();
    }

    private void OnEnable() {
        endScore.text = Manager.Instance.PlayerScore.ToString();
        waveCount.text = Manager.Instance.CurrentWaveNumber.ToString();
        Sound.Instance.PlaySoundClip(SoundEnum.General_Gameover, 1);
    }

}
