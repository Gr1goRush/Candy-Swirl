using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemInputPlayer : MonoBehaviour
{
    [SerializeField] private Button _rightPress;
    [SerializeField] private Button _leftPress;

    public void SubscribeButtomRight(UnityAction action) =>
        _rightPress.onClick.AddListener(action);

    public void SubscribeButtomLeft(UnityAction action) =>
        _leftPress.onClick.AddListener(action);
}
