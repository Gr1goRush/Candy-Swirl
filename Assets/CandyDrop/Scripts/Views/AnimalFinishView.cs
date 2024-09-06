using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalFinishView : MonoBehaviour
{
    [SerializeField] private Sprite _spriteWin;
    [SerializeField] private Image _skin;
    [SerializeField] private Animator _animator;

    public void SetSkinWin() =>
        _skin.sprite = _spriteWin;

    public IEnumerator StartAnimation()
    {
        _animator.SetTrigger("FinishLevel");
        yield return new WaitForSeconds(4);
        _animator.StopPlayback();
    }

    public void SetLocalPosition(Vector3 position) =>
        transform.localPosition = position;

    private void OnValidate()
    {
        if (_skin == null)
            _skin = GetComponent<Image>();

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
