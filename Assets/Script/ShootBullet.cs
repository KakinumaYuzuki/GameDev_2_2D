using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定範囲内にプレイヤーが入ると弾を撃つ
/// 弾を撃つ敵にアタッチ
/// </summary>
public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField, Tooltip("当たり判定の範囲(距離)")]
    private float _radius = 9.0f;

    [SerializeField, Tooltip("球の速さ(xをマイナスにすることで左に飛ぶ)")]
    private Vector3 _bulletSpeed = new Vector3(-5, 0, 0);
    
    private Enemy _enemy;
    private Vector2 _vDistance;
    private float _distance;
    private float _timer = 0.0f;
    private float _interval = 1.0f;
    
    void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (_enemy.Trigger)
        {
            _timer += Time.deltaTime;
            Fire();
        }
    }
    
    /// <summary>
    /// 弾を撃つ（今のところ無限に出るので注意）
    /// </summary>
    private void Fire()
    {
        // 発射間隔を1秒あける
        if (_timer > _interval)
        {
            // 先端から出すため x - 0.5
            var obj = Instantiate(_bulletPrefab, this.transform.position + new Vector3(-0.5f, 0, 0), this.transform.rotation);
            
            // 弾を敵仕様に変更する
            var bullet = obj.GetComponent<Bullet>();
            bullet.SetBulletType(BulletType.Enemy);
            // xがマイナスの場合、左に弾を飛ばす
            bullet.SetDirection(_bulletSpeed);
            
            // メモ：プレイヤーの方向に弾を飛ばす場合
            //Vector3 pos = new Vector3(_player.transform.position.x - this.transform.position.x, 
            //                            _player.transform.position.y - this.transform.position.y, 0);
            //bullet.SetDirection(pos);

            _timer = 0.0f;
        }
    }
}
