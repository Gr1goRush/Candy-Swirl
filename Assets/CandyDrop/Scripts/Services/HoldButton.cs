using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnMouseDrag()
    {
        _button.onClick.Invoke();
    }

    private void OnValidate()
    {
        if (_button == null)
            _button = GetComponent<Button>();
    }
}
