using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WalletManager
{
    public static bool TryPurchase(TypeMoney typeMoney, int price)
    {
        if (typeMoney == TypeMoney.Coins)
        {
            var isMach = ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins >= price;

            if(isMach)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins -= price;

            return isMach;
        }

        if(typeMoney == TypeMoney.Lollipops)
        {
            var isMach = ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop >= price;

            if (isMach)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Lollipop -= price;

            return isMach;
        }

        return false;
    }
}
