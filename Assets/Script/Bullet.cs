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

    private Vector3 _pos;
    private Vector3 _speed = new Vector3(5, 0, 0);
    private int _damageValue = 1;

    public Vector3 Speed { get => _speed; set => _speed = value; }

    private void OnValidate()
    {
        ColorChange();
    }

    private void Start()
    {
        ColorChange();
        _pos = transform.position;
        // 敵の弾のみにを登録する場合は条件式を書く
        GameManager.Instance.Register(this);
    }

    private void Update()
    {
        _pos += _speed * Time.deltaTime;
        transform.position = _pos;

        // 弾の種類によって当たる対象を変え、ダメージを与える
        DealDamage();
    }

    /// <summary>
    /// 判定を行い、ダメージを与える
    /// (出現時のステートによって当たる対象を変える)
    /// 当たっていた場合は自分自身も破棄する
    /// </summary>
    private void DealDamage()
    {
        switch (_type)
        {
            case BulletType.Player:
                var enemyList = GameManager.Instance.Enemies;
                foreach (var enemy in enemyList)
                {
                    if (IsHit(enemy.transform))
                    {
                        enemy.Damage(_damageValue);
                        // ノックバックの方向と威力を設定
                        enemy.SetKnockbackParam(this.transform.position, _damageValue);
                        // 敵に当たった時全体を停止させる(通常弾　0.08s)
                        GameManager.Instance.HitStop(0.08f);
                        DestroyBullet();
                    }
                }
                break;
            case BulletType.Enemy:
                var player = GameManager.Instance.Player;
                if (IsHit(player.transform))
                {
                    player.Damage(_damageValue);
                    // ノックバックの方向と威力を設定
                    player.SetKnockbackParam(this.transform.position, _damageValue);
                    DestroyBullet();
                }
                break;
        }
    }

    /// <summary>
    /// 出現時のステートによって色を変える
    /// </summary>
    private void ColorChange()
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
    /// <param name="target">Transform 弾に当たる判定の対象</param>
    /// <returns>弾に当たっていればtrue、そうでなければfalse</returns>
    private bool IsHit(Transform target)
    {
        var targetLeftBottomPos = target.transform.position - target.transform.localScale / 2;  // 左
        var targetRightUpperPos = target.transform.position + target.transform.localScale / 2;  // 右
        var bulletLeftBottomPos = this.transform.position - this.transform.localScale / 2;      // 左
        var bulletRightUpperPos = this.transform.position + this.transform.localScale / 2;      // 右

        if (targetLeftBottomPos.x <= bulletRightUpperPos.x && bulletLeftBottomPos.x <= targetRightUpperPos.x
                && targetLeftBottomPos.y <= bulletRightUpperPos.y && bulletLeftBottomPos.y <= targetRightUpperPos.y)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 弾の飛ぶ方向を決める
    /// </summary>
    /// <param name="vec"></param>
    public void SetDirection(Vector3 vec)
    {
        _speed = vec.normalized * _speed.magnitude;
    }

    /// <summary>
    /// 弾を消す　(他のオブジェクトとまとめたほうがいいかも)
    /// </summary>
    public void DestroyBullet()
    {
        GameManager.Instance.Unregister(this);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 弾の種類を設定する
    /// </summary>
    /// <param name="type"></param>
    public void SetBulletType(BulletType type)
    {
        _type = type;
    }
}