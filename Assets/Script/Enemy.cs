using UnityEngine;

public class Enemy : Life
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject _bulletPrefab;

    [SerializeField, Tooltip("当たり判定の範囲(距離)")]
    private float _radius = 9.0f;

    [SerializeField, Tooltip("球の速さ(xをマイナスにすることで左に飛ぶ)")]
    private Vector3 _bulletSpeed = new Vector3(-5, 0, 0);

    private Vector2 _vDistance;
    private float _distance;
    private float _timer = 0.0f;
    private float _interval = 1.0f;

    void Start()
    {
        // ゲームマネージャの敵リストに登録
        GameManager.Instance.Register(this);

        // BulletGeneratorがStartにこの処理を書いているため
        // 撃つときにステートを変更する必要がある(逆も然り)
        // メモ：Bulletの方でステートに合わせてオブジェクト自体を変えたほうがいいかも
        /*var bullet = _bulletPrefab.GetComponent<Bullet>();
        bullet.Type = BulletType.Enemy;
        bullet.Speed = _bulletSpeed;*/
    }

    private void Update()
    {
        _vDistance = _player.transform.localPosition - this.transform.position;
        _distance = _vDistance.x * _vDistance.x + _vDistance.y * _vDistance.y;   
        // Playerの中心が円の中に入っていれば
        if (_distance <= _radius * _radius)
        {
            _timer += Time.deltaTime;
            ShootBullet();
            //Debug.Log($"{this.name}範囲内");
        }

        if (Hp <= 0)
        {
            GameManager.Instance.Unregister(this);
            Destroy(this.gameObject);
            return;
        }
    }

    /// <summary>
    /// 球を撃つ（今のところ無限に出るので注意）
    /// </summary>
    private void ShootBullet()
    {
        // 発射間隔を1秒あける
        if (_timer > _interval)
        {
            // 先端から出すため x - 0.5
            var obj = Instantiate(_bulletPrefab, this.transform.position + new Vector3(-0.5f, 0, 0), this.transform.rotation);
            
            // 球を敵仕様に変更する
            var bullet = obj.GetComponent<Bullet>();
            bullet.Type = BulletType.Enemy;
            bullet.Speed = _bulletSpeed;

            _timer = 0.0f;
        }
    }
}