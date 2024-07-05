using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject _bulletPrefab;

    [SerializeField, Tooltip("当たり判定の範囲(距離)")]
    private float _radius = 9.0f;

    [SerializeField, Tooltip("球の速さ(マイナスにすることで左に飛ぶ)")]
    private float _bulletSpeed = -5.0f;

    private Vector2 _vDistance;
    private float _distance;
    private float _timer = 0.0f;
    private float _interval = 1.0f;


    // Start is called before the first frame update
    /*void Start()
    {
        // BulletGeneratorがStartにこの処理を書いているため
        // 撃つときにステートを変更する必要がある(逆も然り)
        // メモ：Bulletの方でステートに合わせてオブジェクト自体を変えたほうがいいかも
        var bullet = _bulletPrefab.GetComponent<Bullet>();
        bullet.Type = BulletType.Enemy;
        bullet.Speed = _bulletSpeed;
    }*/

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
