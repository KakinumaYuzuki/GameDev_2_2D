using UnityEngine;

/// <summary>
/// 一定範囲内でプレイヤーに向かってくる敵
/// Enemyの範囲判定を使用(継承したほうがいいかも？)
/// </summary>
public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private PlayerController _player;
    private Enemy _enemy;
    private Vector3 _direction;
    private Vector3 _vec = Vector3.zero;
    private bool _canDealDamage = true;
    private int _damageValue = 1;
    
    void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerController>();
        _enemy = GetComponent<Enemy>();
        // 最初から範囲内だった場合
        _vec = (_player.transform.position - this.transform.position).normalized;
    }
    
    void Update()
    {
        // ノックバック
        _enemy.Knockback();
        
        if (_enemy.Trigger)
        {
            this.transform.position += _speed * Time.deltaTime * _vec;
            if (IsHit())
            {
                if (_canDealDamage)
                {
                    _player.Damage(_damageValue);
                    // ノックバックの方向と威力を設定
                    _player.SetKnockbackParam(this.transform.position, _damageValue);
                    _canDealDamage = false;  // 一度しか当たらない想定
                }
            }
        }
        else
        {
            _vec = (_player.transform.position - this.transform.position).normalized;
        }
    }

    /// <summary>
    /// プレイヤーとの当たり判定をとる
    /// </summary>
    /// <returns>当たっていればtrue、そうでなければfalse</returns>
    /// (後で共通化するかも)
    private bool IsHit()
    {
        var playerLeftBottomPos = _player.transform.position - _player.transform.localScale / 2;  // 左
        var playerRightUpperPos = _player.transform.position + _player.transform.localScale / 2;  // 右
        var bulletLeftBottomPos = this.transform.position - this.transform.localScale / 2;      // 左
        var bulletRightUpperPos = this.transform.position + this.transform.localScale / 2;      // 右

        if (playerLeftBottomPos.x <= bulletRightUpperPos.x && bulletLeftBottomPos.x <= playerRightUpperPos.x
                                                           && playerLeftBottomPos.y <= bulletRightUpperPos.y && bulletLeftBottomPos.y <= playerRightUpperPos.y)
        {
            Debug.Log("接触");
            return true;
        }

        return false;
    }
}
