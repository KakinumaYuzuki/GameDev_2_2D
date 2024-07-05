using UnityEngine;

public enum BulletType
{
    Player,
    Enemy
}

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletType _type;

    [SerializeField]
    private Color _playerBulletColor;

    [SerializeField]
    private Color _enemyBulletColor;

    private Vector2 _pos;
    private float _speed = 5.0f;

    public float Speed { get => _speed; set => _speed = value; }

    public BulletType Type
    {
        get => _type;
        set
        {
            _type = value;
            OnTypeChanged();
        }
    }

    private void OnValidate()
    {
        OnTypeChanged();
    }

    private void Start()
    {
        _pos = transform.position;
    }

    private void Update()
    {
        _pos.x += _speed * Time.deltaTime;
        transform.position = _pos;
    }

    private void OnTypeChanged()
    {
        var renderer = GetComponent<SpriteRenderer>();
        switch (_type)
        {
            case BulletType.Player:
                renderer.color = _playerBulletColor; 
                break;
            case BulletType.Enemy:
                renderer.color = _enemyBulletColor;
                break;
        }
    }
}
