using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private TextMeshProUGUI _viewCoins;
    [SerializeField] private List<SkinView> _skins;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _back.onClick.AddListener(() => { gameObject.SetActive(false); });
        UpdateViewCoin();

        foreach (var skin in _skins)
        {
            skin.OnPurchasedEventHandler.AddListener(UpdateViewCoin);
            skin.OnPurchasedEventHandler.AddListener(UpdateStateProducts);
            skin.OnSelectedEventHandler.AddListener(UpdateStateProducts);
        }
    }

    private void UpdateStateProducts()
    {
        foreach (var skin in _skins)
            skin.InitializeStateSkin();
    }

    private void UpdateViewCoin() =>
        _viewCoins.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
}
