
public class IngameMenu : Menu {

    public void PlayClickSound() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
    }

    public void Resume() {
        Hide();
        PlayClickSound();
        EventManager.Instance.EndPause();
    }

    public void ShowOptions() {
        Manager.Instance.Options.Show();
    }

    public void QuitRunningGame() {
        PlayClickSound();
        Hide();
        EventManager.Instance.QuitGame();
        EventManager.Instance.GameReset();
        Manager.Instance.BeforeGameScreen.SetActive(true);
        Manager.Instance.MainMenu.Show();
    }
}
