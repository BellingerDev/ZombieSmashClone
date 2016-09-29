namespace UI.Screens
{
    public class UIHUDScreen : UIScreen
    {
        public override void Show()
        {
            this.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
