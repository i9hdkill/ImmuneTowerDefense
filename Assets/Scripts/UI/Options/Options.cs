
public class Options : Menu {

    public void Back() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
        Hide();
        if (Manager.Instance.IsStarted) {
            Sound.Instance.SaveSoundVolumeIngame();
            return;
        }
        Manager.Instance.MainMenu.Show();
        Manager.Instance.BeforeGameScreen.SetActive(true);
    }
}
