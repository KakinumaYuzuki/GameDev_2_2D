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

    PlayerController _player;

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
        _player = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        _pos.x += _speed * Time.deltaTime;
        transform.position = _pos;

        // ìGÇÃíeÇ™ÉvÉåÉCÉÑÅ[Ç…ìñÇΩÇ¡ÇƒÇ¢ÇÈÇ©
        // ãÈå`ìØémÇÃîªíË
        if (_type == BulletType.Enemy)
        {
            var playerLeftUpperPos = _player.transform.position - _player.transform.localScale / 2;
            var playerRightBottomPos = _player.transform.position + _player.transform.localScale / 2;
            var bulletLeftUpperPos = this.transform.position - this.transform.localScale / 2;
            var bulletBottomPos = this.transform.position + this.transform.localScale / 2;
            if (playerLeftUpperPos.x <= bulletBottomPos.x && bulletLeftUpperPos.x <= playerRightBottomPos.x
                    && playerLeftUpperPos.y <= bulletBottomPos.y && bulletLeftUpperPos.y <= playerRightBottomPos.y)
            {
                _player.Damage();
                Destroy(this.gameObject);
            }
        }
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
