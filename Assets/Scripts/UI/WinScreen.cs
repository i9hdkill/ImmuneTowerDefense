using UnityEngine;
using UnityEngine.UI;

public class WinScreen : Menu {

    [SerializeField]
    private Text endScore;

    private void OnEnable() {
        endScore.text = Manager.Instance.PlayerScore.ToString();
    }

    public void BackToMainMenu() {
        Hide();
        Manager.Instance.TopBar.Show();
        Sound.Instance.PlayMusicClip(MusicEnum.MenuLoop, true);
        Manager.Instance.MainMenu.Show();
        Manager.Instance.BeforeGameScreen.SetActive(true);
        EventManager.Instance.GameReset();
    }

}
