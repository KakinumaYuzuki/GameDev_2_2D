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
            OnColorChanged();
        }
    }

    private void OnValidate()
    {
        OnColorChanged();
    }

    private void Start()
    {
        _pos = transform.position;
    }

    private void Update()
    {
        _pos.x += _speed * Time.deltaTime;
        transform.position = _pos;

        // 球の種類によって当たる対象を変える
        switch (_type)
        {
            case BulletType.Player:
                var enemyList = GameManager.Instance.Enemies;
                foreach (var enemy in enemyList)
                {
                    if (IsHit(enemy.transform))
                    {
                        enemy.Damage();
                        Destroy(this.gameObject);
                    }
                }
                break;
            case BulletType.Enemy:
                var player = GameManager.Instance.Player;
                if (IsHit(player.transform))
                {
                    player.Damage();
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    /// <summary>
    /// 出現時のステートによって色を変える
    /// </summary>
    private void OnColorChanged()
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

    /// <summary>
    /// 当たり判定をとる 矩形同士の判定
    /// </summary>
    /// <param name="target">Transform 球に当たる判定の対象</param>
    /// <returns>球に当たっていればtrue、そうでなければfalse</returns>
    private bool IsHit(Transform target)
    {
        var targetLeftUpperPos = target.transform.position - target.transform.localScale / 2;
        var playerRightBottomPos = target.transform.position + target.transform.localScale / 2;
        var bulletLeftUpperPos = this.transform.position - this.transform.localScale / 2;
        var bulletBottomPos = this.transform.position + this.transform.localScale / 2;

        if (targetLeftUpperPos.x <= bulletBottomPos.x && bulletLeftUpperPos.x <= playerRightBottomPos.x
                && targetLeftUpperPos.y <= bulletBottomPos.y && bulletLeftUpperPos.y <= playerRightBottomPos.y)
        {
            return true;
        }
        return false;
    }
}
