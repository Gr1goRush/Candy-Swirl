using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    private bool _isDead;

    [Header("Components")]
    [SerializeField] private Image _skin;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private EnemyDamageView _bodyEnemy;
    [Header("Скин смерти")]
    [SerializeField] private Image _skinDeath;

    public bool IsDead => _isDead;

    public void SetSkin(Sprite spriteSkin) =>
        _skin.sprite = spriteSkin;

    public void SetLocalPosition(Vector3 position) =>
        transform.localPosition = position;

    public void Death()
    {
        _skinDeath.enabled = true;
        _bodyEnemy.gameObject.SetActive(false);
        var sizeDeath = new Vector2(100, 30);
        _rectTransform.sizeDelta = sizeDeath;
        _boxCollider.size = sizeDeath;
        _isDead = true;
    }

    private void Start()
    {
        _isDead = false;
    }

    private void OnValidate()
    {
        if (_skin == null)
            _skin = GetComponent<Image>();

        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider2D>();

        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }
}
