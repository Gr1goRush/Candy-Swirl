using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsView : MonoBehaviour
{
    [SerializeField] private Button _openPanelLevels;
    [SerializeField] private Button _back;
    [SerializeField] private TextMeshProUGUI _viewLollipop;
    [SerializeField] private GameObject _panelLevels;
    [SerializeField] private List<LevelItemView> _levels;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _openPanelLevels.onClick.AddListener(() => { _panelLevels.SetActive(true); });
        _back.onClick.AddListener(() =>
        {
            AudioManager.Instance.ClickButton();
            _panelLevels.SetActive(false);
        });
        UpdateLollipops();

        foreach (var level in _levels)
            level.OnPurchasedEventHandler.AddListener(UpdateLollipops);
    }

    private void UpdateLollipops() =>
        _viewLollipop.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop.ToString();

    private void OnValidate()
    {
        if (_openPanelLevels == null)
            _openPanelLevels = GetComponent<Button>();
    }
}
