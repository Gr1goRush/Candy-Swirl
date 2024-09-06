using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private int _countHealth = 1;

    public int CountHealth => _countHealth;

    public void GetHealth()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Health += _countHealth;
        gameObject.SetActive(false);
    }

    public void DiactivateHealth() =>
        gameObject.SetActive(false);

    public void SetLocalPosition(Vector2 position) =>
        transform.localPosition = position;
}
