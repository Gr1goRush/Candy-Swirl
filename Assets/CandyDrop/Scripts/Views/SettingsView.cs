using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _back;

    private void Start()
    {
        _back.onClick.AddListener(() => { gameObject.SetActive(false); });
    }
}
