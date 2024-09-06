using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _playAgain;
    [SerializeField] private Button _menu;

    public void SetActive(bool value) =>
        gameObject.SetActive(value);

    private void Start()
    {
        _playAgain.onClick.AddListener(OnPlayAgain);
        _menu.onClick.AddListener(OnGoToMenu);
    }

    private void OnPlayAgain()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnGoToMenu()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
