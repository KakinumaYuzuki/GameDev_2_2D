using UnityEngine;

/// <summary>
/// ライフとノックバック管理
/// ライフを持つクラスで継承する
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField]
    private int _hp = 5;

    [SerializeField]
    private bool _isKnockback = false;

    [SerializeField]
    private Vector3 _knockbackDirection;

    private float _knockbackTimer = 0.15f;  // ノックバックを行う時間
    private Vector3 _pos = Vector3.zero;
    private float _knockbackPower = 1.0f;

    public int Hp => _hp;
    public bool IsKnockback => _isKnockback;

    public virtual void Damage(int value)
    {
        Debug.Log($"{this.gameObject}にダメージ{value}");
        _hp -= value;
        _isKnockback = true;
        _knockbackTimer = 0.15f;
        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log($"{this.gameObject}ライフ0");
        }
    }

    /// <summary>
    /// ノックバックの処理(現状ｘ方向のみ)
    /// </summary>
    public void Knockback()
    {
        if (!_isKnockback)
        {
            return;
        }
        Debug.Log("ノックバック");

        _pos = this.transform.position;
        _pos.x = _knockbackDirection.x * Time.deltaTime * _knockbackPower;
        this.transform.position += new Vector3(_pos.x, 0, 0);
        
        _knockbackTimer -= Time.deltaTime;
        if (_knockbackTimer < 0)
        {
            _isKnockback = false;
        }
    }
    
    /// <summary>
    /// ノックバックの方向と力を設定する
    /// </summary>
    /// <param name="pos">呼び出す側のポジション</param>
    /// <param name="power">ダメージ力</param>
    /// <remarks>当たってくる側から呼び出す</remarks>
    public void SetKnockbackParam(Vector3 pos, float power)
    {
        _knockbackDirection = (this.transform.position - pos).normalized;
        _knockbackPower = power * 5;    // power = 1の場合わかりづらいので調整
    }
}
