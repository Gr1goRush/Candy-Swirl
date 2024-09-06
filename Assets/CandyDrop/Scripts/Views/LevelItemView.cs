using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class LevelItemView : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _price;
    [SerializeField] private TypeMoney _typeMoney;
    [SerializeField] private Button _play;
    [SerializeField] private Button _purchase;
    [SerializeField] private TextMeshProUGUI _priceView;

    public UnityEvent OnPurchasedEventHandler = new UnityEvent();

    private void Start()
    {
        if(_priceView != null)
            _priceView.text = _price.ToString();
        if (_purchase != null)
        {
            _purchase.onClick.AddListener(() =>
            {
                AudioManager.Instance.ClickButton();
                OnPurchase();
            });
            CheckOnPurchase();
        }
        if (_play != null)
            _play.onClick.AddListener(() =>
            {
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Level = _numberLevel;
                AudioManager.Instance.ClickButton();
                ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
            });
    }

    private void CheckOnPurchase()
    {
        var openLevels = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenLevels;
        var arrayOpenLevels = openLevels.Split(",");
        for (int i = 0; i < arrayOpenLevels.Length; i++)
        {
            if (arrayOpenLevels[i] == _numberLevel.ToString())
            {
                _purchase.gameObject.SetActive(false);
                _play.gameObject.SetActive(true);
            }
        }
    }

    private void OnPurchase()
    {
        if (WalletManager.TryPurchase(_typeMoney, _price))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenLevels += "," + _numberLevel.ToString();
            _purchase.gameObject.SetActive(false);
            _play.gameObject.SetActive(true);
            OnPurchasedEventHandler?.Invoke();
        }
    }
}
