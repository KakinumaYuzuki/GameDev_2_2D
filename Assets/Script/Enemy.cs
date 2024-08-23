using UnityEngine;

public class Enemy : Character
{
    [SerializeField, Tooltip("当たり判定の範囲(距離)")]
    private float _radius = 9.0f;
    
    private PlayerController _player;
    private Vector2 _vDistance;
    private float _distance;
    private bool _trigger = false;  // プレイヤーが判定内にいるかどうか
    
    public bool Trigger => _trigger;

    void Start()
    {
        // ゲームマネージャの敵リストに登録
        GameManager.Instance.Register(this);
        
        _player = GameObject.FindObjectOfType<PlayerController>();
        
        // スポナーの子オブジェクトになるのを回避
        this.gameObject.transform.parent = null;
    }

    private void Update()
    {
        // ノックバック
        Knockback();
        
        _vDistance = _player.transform.localPosition - this.transform.position;
        _distance = _vDistance.x * _vDistance.x + _vDistance.y * _vDistance.y;   
        // Playerの中心が円の中に入っていれば
        if (_distance <= _radius * _radius)
        {
            _trigger = true;
            Debug.Log($"{this.name}範囲内");
        }
        else
        {
            _trigger = false;
        }
        if (Hp <= 0)
        {
            GameManager.Instance.Unregister(this);
            Destroy(this.gameObject);
            return;
        }
    }
    
    /// <summary>
    /// 画面外に行ったら自身を破棄する
    /// </summary>
    private void OnBecameInvisible()
    {
        GameManager.Instance.Unregister(this);
        Destroy(this.gameObject);
    }
}