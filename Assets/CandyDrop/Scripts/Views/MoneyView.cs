using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TypeMoney _typeMoney;
    [SerializeField] private TextMeshProUGUI _viewMovey;

    private void Start()
    {
        switch(_typeMoney)
        {
            case TypeMoney.Coins:
                _viewMovey.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
                break;
            case TypeMoney.Lollipops:
                _viewMovey.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop.ToString();
                break;
        }
    }
}
