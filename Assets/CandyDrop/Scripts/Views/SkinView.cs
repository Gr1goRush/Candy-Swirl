using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkinView : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Skin _skin;
    [Header("Components")]
    [SerializeField] private Button _purchase;
    [SerializeField] private Button _select;
    [SerializeField] private Button _selected;
    [SerializeField] private TextMeshProUGUI _priceView;

    [HideInInspector]
    public UnityEvent OnPurchasedEventHandler = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSelectedEventHandler = new UnityEvent();

    public void InitializeStateSkin()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin == _skin.Name)
        {
            SetSpriteSelected();
            return;
        }

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins.Contains(_skin.Name))
            SetSpriteSelect();
    }

    private void Start()
    {
        InitializeStateSkin();
        _purchase.onClick.AddListener(() => { OnPurchase(); });
        _select.onClick.AddListener(() => { OnSelect(); });
        _priceView.text = _price.ToString();
    }

    private void OnPurchase()
    {
        if (WalletManager.TryPurchase(TypeMoney.Coins, _price))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins += _skin.Name + ",";
            OnPurchasedEventHandler?.Invoke();
        }
    }

    private void OnSelect()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin = _skin.Name;
        OnSelectedEventHandler?.Invoke();
    }

    private void SetSpriteSelected()
    {
        _purchase.gameObject.SetActive(false);
        _select.gameObject.SetActive(false);
        _selected.gameObject.SetActive(true);
    }

    private void SetSpriteSelect()
    {
        _purchase.gameObject.SetActive(false);
        _select.gameObject.SetActive(true);
        _selected.gameObject.SetActive(false);
    }
}
