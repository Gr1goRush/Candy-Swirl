using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeView : MonoBehaviour
{
    [SerializeField] private Slider _soundVolume;

    private void Start()
    {
        _soundVolume.value = AudioManager.Instance.SoundVolume;
        _soundVolume.onValueChanged.AddListener((value) => { AudioManager.Instance.ChangeSoundVolume(value); });
    }

    private void OnValidate()
    {
        if (_soundVolume == null)
            _soundVolume = GetComponent<Slider>();
    }
}
