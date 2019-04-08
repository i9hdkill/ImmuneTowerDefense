using UnityEngine;

public class MainMenu : Menu {

    public void StartGame() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
        Manager.Instance.BeforeGameScreen.SetActive(false);
        Hide();
        EventManager.Instance.StartGame();
    }

    public void ShowOptions() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
        Hide();
        Manager.Instance.Options.Show();
    }

    public void QuitGame() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
        Application.Quit();
    }
}
