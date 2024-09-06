using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationView : MonoBehaviour
{
    [SerializeField] private Button _close;

    private void Start()
    {
        _close.onClick.AddListener(() =>
        {
            AudioManager.Instance.ClickButton();
            gameObject.SetActive(false);
        });
    }

    private void OnValidate()
    {
        if (_close == null)
            _close = GetComponent<Button>();
    }
}
