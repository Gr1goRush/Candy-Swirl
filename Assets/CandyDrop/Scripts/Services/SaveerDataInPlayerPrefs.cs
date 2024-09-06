using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveerDataInPlayerPrefs : SaveerData
{
    private readonly string KEY_NUMBER_LEVEL = "Level";
    private readonly string KEY_COUNT_LOLLIPOP = "Lollipop";
    private readonly string KEY_COUNT_COIN = "Coins";
    private readonly string KEY_OPEN_LEVELS = "OpenLevels";
    private readonly string KEY_IS_MUSIC_ON = "IsMusicOn";
    private readonly string KEY_IS_VIBRATION_ON = "IsVibrationOn";
    private readonly string KEY_SOUND_VOLUME = "SoundVolume";
    private readonly string KEY_OPEN_SKINS = "OpenSkins";
    private readonly string KEY_CURRENT_SKINS = "CurrentSkin";
    private readonly string KEY_COUNT_HELTH = "Health";

    public int Level { get { return Load<int>(KEY_NUMBER_LEVEL, 1); } set { Save<int>(KEY_NUMBER_LEVEL, value); } }
    public int Lollipop { get { return Load<int>(KEY_COUNT_LOLLIPOP, 0); } set { Save<int>(KEY_COUNT_LOLLIPOP, value); } }
    public int Coins { get { return Load<int>(KEY_COUNT_COIN, 0); } set { Save<int>(KEY_COUNT_COIN, value); } }
    public string OpenLevels { get { return Load<string>(KEY_OPEN_LEVELS, "1"); } set { Save<string>(KEY_OPEN_LEVELS, value); } }
    public string IsMusicOn { get { return Load<string>(KEY_IS_MUSIC_ON, "1"); } set { Save<string>(KEY_IS_MUSIC_ON, value); } }
    public string IsVibrationOn { get { return Load<string>(KEY_IS_VIBRATION_ON, "1"); } set { Save<string>(KEY_IS_VIBRATION_ON, value); } }
    public float SoundVolume { get { return Load<float>(KEY_SOUND_VOLUME, 1); } set { Save<float>(KEY_SOUND_VOLUME, value); } }
    public string OpenSkins { get { return Load<string>(KEY_OPEN_SKINS, "Skin1"); } set { Save<string>(KEY_OPEN_SKINS, value); } }
    public string CurrentSkin { get { return Load<string>(KEY_CURRENT_SKINS, "Skin1"); } set { Save<string>(KEY_CURRENT_SKINS, value); } }
    public int Health { get { return Load<int>(KEY_COUNT_HELTH, 1); } set { Save<int>(KEY_COUNT_HELTH, value); } }

    public override T Load<T>(string nameParameter, T defaultValue)
    {
        if (PlayerPrefs.HasKey(nameParameter) == false)
            return defaultValue;

        Type inType = typeof(T);

        if (inType == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(nameParameter);
        else if (inType == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(nameParameter);
        else
            return (T)(object)PlayerPrefs.GetString(nameParameter);
    }

    public override void Save<T>(string nameParameter, T value)
    {
        Type inType = typeof(T);

        if (inType == typeof(int))
            PlayerPrefs.SetInt(nameParameter, int.Parse(value.ToString()));
        else if (inType == typeof(float))
            PlayerPrefs.SetFloat(nameParameter, float.Parse(value.ToString()));
        else if (inType == typeof(string))
            PlayerPrefs.SetString(nameParameter, value.ToString());
    }
}
