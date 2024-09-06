using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;
    [SerializeField] private List<Sound> _sounds;

    public static AudioManager Instance { get; private set; }
    public float SoundVolume => ContainerSaveerPlayerPrefs.Instance.SaveerData.SoundVolume;

    public void ChangeStateAudio()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == "1")
            PlayAudio();
        else
            StopAudio();
    }

    public void StopAudio()
    {
        _audioSourceMusic.Stop();
        _audioSourceSound.Stop();
    }

    public void PlayAudio()
    {
        _audioSourceMusic.Play();
        _audioSourceSound.Play();
    }

    public void PlaySourceSound() =>
        _audioSourceSound.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.SoundVolume;

    public void PauseSourceSound() =>
        _audioSourceSound.volume = 0;

    public bool IsAudioOn() =>
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == "1" ? true : false;

    public void ChangeSoundVolume(float soundValue)
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.SoundVolume = soundValue;
        _audioSourceMusic.volume = soundValue;
        _audioSourceSound.volume = soundValue;
    }

    public void ClickButton() => PlaySound("ButtonClick");

    public void PlayGetBonus() => PlaySound("Bonus");

    public void PlayBaseJump() => PlaySound("BaseJump");

    public void PlayJumpToEnemy() => PlaySound("JumpToEnemy");

    public void PlayWinner() => PlaySound("Winner");

    public void PlayGameOver() => PlaySound("GameOver");

    private void PlaySound(string name)
    {
        var sound = FindSound(name);

        if (sound != null && IsAudioOn())
        {
            _audioSourceSound.clip = sound.Music;
            _audioSourceSound.Play();
        }
    }

    private Sound FindSound(string name) =>
        _sounds.Find(sound => sound.Name == name);

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (IsAudioOn() == false)
            StopAudio();

        _audioSourceMusic.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.SoundVolume;
        _audioSourceSound.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.SoundVolume;

        ManagerScenes.Instance.StartLoadingSceneEventHandler.AddListener(() => { PauseSourceSound(); });
    }
}