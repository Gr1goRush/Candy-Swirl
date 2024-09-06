using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Animator _animatorLoading;

    public void OnStarLoadingScene()
    {
        gameObject.SetActive(true);
        _animatorLoading.StartRecording(0);
    }

    public IEnumerator StartLoading()
    {
        while (_animatorLoading.GetCurrentAnimatorStateInfo(0).normalizedTime != _animatorLoading.GetCurrentAnimatorStateInfo(0).length)
        {
            if (_animatorLoading.GetCurrentAnimatorStateInfo(0).normalizedTime >= _animatorLoading.GetCurrentAnimatorStateInfo(0).length)
            {
                _animatorLoading.StopPlayback();
                gameObject.SetActive(false);
                AudioManager.Instance.PlaySourceSound();
            }

            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(StartLoading());
    }
}
