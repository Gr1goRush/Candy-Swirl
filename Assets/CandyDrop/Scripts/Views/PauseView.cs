using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PauseView : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _replay;
    [SerializeField] private Button _menu;

    public ReactiveCommand<bool> OnPauseCommand = new();

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable<bool>(OnPauseCommand);
        _continue.onClick.AddListener(OnContinue);
        _replay.onClick.AddListener(OnReplay);
        _menu.onClick.AddListener(OnGoToMenu);
    }

    private void OnEnable()
    {
        OnPauseCommand.Execute(gameObject.activeSelf);
    }

    private void OnContinue()
    {
        OnPauseCommand.Execute(false);
        gameObject.SetActive(false);
    }

    private void OnReplay()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnGoToMenu()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnPauseCommand);
    }
}
