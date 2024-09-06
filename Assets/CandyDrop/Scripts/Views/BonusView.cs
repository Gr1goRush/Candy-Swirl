using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusView : MonoBehaviour
{
    [SerializeField] private CoinsView _coinPrefab;
    [SerializeField] private TextMeshProUGUI _coinsView;
    [SerializeField] private TextMeshProUGUI _coinsTarget;
    [SerializeField] private LollipopView _lollipopPrefab;
    [SerializeField] private TextMeshProUGUI _lollipopView;
    [SerializeField] private TextMeshProUGUI _lollipopTarget;

    public CoinsView CoinPrefab => _coinPrefab;
    public TextMeshProUGUI CoinsView => _coinsView;
    public TextMeshProUGUI CoinsTarget => _coinsTarget;
    public LollipopView LollipopPrefab => _lollipopPrefab;
    public TextMeshProUGUI LollipopView => _lollipopView;
    public TextMeshProUGUI LollipopTarget => _lollipopTarget;

    public void UpdateCoinsView(int coundCoins) =>
        _coinsView.text = (int.Parse(_coinsView.text) + coundCoins).ToString();

    public void UpdateLollipopView(int countLollipops) =>
    _lollipopView.text = (int.Parse(_lollipopView.text) + countLollipops).ToString();
}
