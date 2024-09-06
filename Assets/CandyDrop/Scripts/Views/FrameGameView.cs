using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameGameView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _imageGround;

    public float Width => _rectTransform.sizeDelta.x;

    public float Height => _rectTransform.sizeDelta.y;

    public void SetPositionBottom(float x) =>
        transform.localPosition = new Vector3(x, -133, 0);

    public void FlipHorizontally() =>
        transform.Rotate(0, 180, 0);

    public void SetGround(Sprite spriteGround) =>
        _imageGround.sprite = spriteGround;

    private void OnValidate()
    {
        if (_rectTransform == null)
            _rectTransform = transform.GetComponent<RectTransform>();

        if (_imageGround == null)
            _imageGround = transform.GetComponent<Image>();
    }
}
