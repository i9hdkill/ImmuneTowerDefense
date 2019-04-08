
public class TopBar : Menu {

    public void OpenPopup() {
        Manager.Instance.IngameMenu.Show();
        EventManager.Instance.StartPause();
    }

}
