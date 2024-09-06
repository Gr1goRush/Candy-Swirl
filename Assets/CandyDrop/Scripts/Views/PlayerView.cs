using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerView : MonoBehaviour
{
    private Transform _transform;
    private bool _collisionWallLeft;
    private bool _collisionWallRight;

    [Header("Мин. дист. позиции назначения")]
    public FloatReactiveProperty MinDistancePositionTraget = new();
    [Header("Сила прыжка")]
    public FloatReactiveProperty ForceJump = new();
    [Header("Чувствительность движения")]
    public FloatReactiveProperty SensitivityMove = new();
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Image _skin;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Image> _faces;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private int _maxHealth = 2;

    public ReactiveCommand OnDestroyCommand { get; private set; } = new();
    public ReactiveCommand OnCollisionEnterCommand { get; private set; } = new();
    public ReactiveCommand OnKilledEnemyCommand { get; private set; } = new();
    public ReactiveCommand OnWinCommand { get; private set; } = new();
    public ReactiveCommand OnGameOverCommand { get; private set; } = new();
    public ReactiveCommand OnGetLollipopCommand { get; private set; } = new();
    public ReactiveCommand OnUpdateHealthCommand { get; private set; } = new();
    public Vector2 Position => _transform.position;
    public bool CollisionWallLeft => _collisionWallLeft;
    public bool CollisionWallRight => _collisionWallRight;

    public void ResetCollisionWallLeft() =>
        _collisionWallLeft = false;

    public void ResetCollisionWallRight() =>
        _collisionWallRight = false;

    public void LoaidngSkin()
    {
        var findedSkin = _skins.Find(skin => skin.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin);
        _skin.sprite = findedSkin.SkinImage;
    }

    public void UpdatePosition(Vector2 target) =>
        _transform.position = target;

    public void UpdateVelocity(Vector2 velocity) =>
        _rigidbody2D.velocity = velocity;

    public void PauseMove()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animator.StartPlayback();
        _collisionWallLeft = true;
        _collisionWallRight = true;
    }

    public void ContinueMove()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _animator.StopPlayback();
        ResetCollisionWallLeft();
        ResetCollisionWallRight();
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _collisionWallLeft = false;
        _collisionWallRight = false;
        var indexSkin = int.Parse(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin.Substring(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin.Length - 1, 1));
        _animator.SetInteger("Skin", indexSkin);
        _faces[indexSkin - 1].gameObject.SetActive(true);

        if (indexSkin == 2)
            _rectTransform.sizeDelta = new Vector2(126, 98);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var wall = collision.gameObject.GetComponent<WallView>();
        if (wall != null)
        {
            if (wall.TypeWall == TypeWall.Left)
                _collisionWallLeft = true;
            else
                _collisionWallRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthView>();
        if (health != null)
        {
            if (ContainerSaveerPlayerPrefs.Instance.SaveerData.Health < _maxHealth)
            {
                health.GetHealth();
                OnUpdateHealthCommand.Execute();
            }
            else
                health.DiactivateHealth();
        }

        var animalFinish = collision.gameObject.GetComponent<AnimalFinishView>();
        if (animalFinish != null)
        {
            StartCoroutine(OnFinish(animalFinish));
            return;
        }

        var enemyDanage = collision.collider.gameObject.GetComponent<EnemyDamageView>();

        if (enemyDanage != null)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Health -= enemyDanage.Damage;
            OnUpdateHealthCommand.Execute();

            if (ContainerSaveerPlayerPrefs.Instance.SaveerData.Health == 0)
            {
                OnGameOverCommand.Execute();
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Health = 1;
                return;
            }
        }

        var enemy = collision.collider.gameObject.GetComponent<EnemyView>();

        if (enemy != null && enemy.IsDead == false)
        {
            enemy.Death();
            OnKilledEnemyCommand.Execute();
            AudioManager.Instance.PlayJumpToEnemy();
        }
        else
            AudioManager.Instance.PlayBaseJump();

        var platform = collision.collider.gameObject.GetComponent<PlatformView>();
        var frameGame = collision.collider.gameObject.GetComponent<FrameGameView>();

        if (platform != null || enemy != null || frameGame != null)
        {
            _animator.SetTrigger("Jump");
            OnCollisionEnterCommand.Execute();
        }
    }

    private IEnumerator OnFinish(AnimalFinishView animalFinishView)
    {
        PauseMove();
        OnGetLollipopCommand.Execute();
        yield return StartCoroutine(animalFinishView.StartAnimation());
        OnWinCommand.Execute();
        AudioManager.Instance.PlayWinner();
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_skin == null)
            _skin = GetComponent<Image>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        OnDestroyCommand.Execute();
    }
}
