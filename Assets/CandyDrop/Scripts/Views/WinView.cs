using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinView : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _menu;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _trophy;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private TextMeshProUGUI _lollipops;
    [SerializeField] private TextMeshProUGUI _time;
    [Header("Данные из игры")]
    [SerializeField] private TextMeshProUGUI _healthGame;
    [SerializeField] private TextMeshProUGUI _trophyGame;
    [SerializeField] private TextMeshProUGUI _coinsGame;
    [SerializeField] private TextMeshProUGUI _lollipopsGame;
    [SerializeField] private TextMeshProUGUI _timeGame;

    public void SetActive(bool value) =>
        transform.gameObject.SetActive(value);

    private void Start()
    {
        _continue.onClick.AddListener(OnContinue);
        _menu.onClick.AddListener(OnGoToMenu);
    }

    private void OnEnable()
    {
        ViewDataBar();
    }

    private void ViewDataBar()
    {
        _health.text = _healthGame.text;
        _trophy.text = _trophyGame.text;
        _coins.text = _coinsGame.text;
        _lollipops.text = _lollipopsGame.text;
        _time.text = _timeGame.text;
    }

    private void OnContinue()
    {
        var openLevels = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenLevels;
        var arrayOpenLevels = openLevels.TrimEnd().Split(",");
        for (int i = 0; i < arrayOpenLevels.Length; i++)
        {
            var openedLevel = int.Parse(arrayOpenLevels[i]);
            if (openedLevel > ContainerSaveerPlayerPrefs.Instance.SaveerData.Level)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Level = openedLevel;
        }

        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnGoToMenu()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
