public class MusicSwitchView : SwitchView
{
    public override void Switch()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn = AudioManager.Instance.IsAudioOn() ? "0" : "1";
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn);
        AudioManager.Instance.ChangeStateAudio();
    }

    protected override void Start()
    {
        base.Start();
        ChangeSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn);
    }
}
