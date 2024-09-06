using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesController
{
    private BonusView _bonusView;

    public BonusesController(BonusView bonusView)
    {
        _bonusView = bonusView;
    }

    public void OnEnemyDeath(Transform parent, Vector2 startPosition)
    {
        var coin = PoolObjects<CoinsView>.GetObject(_bonusView.CoinPrefab, parent);
        coin.transform.position = startPosition;
        coin.SetPositionBonusView(_bonusView.CoinsTarget.transform.position);
        int countBonus = 1;
        coin.UpdateBonusEventHandler.AddListener(() =>
        {
            if(AudioManager.Instance != null)
                AudioManager.Instance.PlayGetBonus();

            _bonusView.UpdateCoinsView(countBonus);
        });
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins + countBonus;
    }

    public void OnFinishLevel(Transform parent, Vector2 startPosition)
    {
        var lollipop = PoolObjects<LollipopView>.GetObject(_bonusView.LollipopPrefab, parent);
        lollipop.transform.position = startPosition;
        lollipop.SetPositionBonusView(_bonusView.LollipopTarget.transform.position);
        int countBonus = 1;
        lollipop.UpdateBonusEventHandler.AddListener(() =>
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayGetBonus();

            _bonusView.UpdateLollipopView(countBonus);
        });
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop = ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop + countBonus;
    }
}
