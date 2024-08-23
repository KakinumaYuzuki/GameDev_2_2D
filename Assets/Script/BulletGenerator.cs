using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Playerの弾を生成する
/// </summary>
public class BulletGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [FormerlySerializedAs("_lazerBulletPrefab")] [SerializeField]
    private GameObject _laserBulletPrefab;

    [SerializeField]
    private GameObject _player;
    
    [SerializeField, Tooltip("ポインター")]
    private GameObject _target;
    private float _radian = 0.0f;

    private float _timer;

    private void Start()
    {
        var bullet = _bulletPrefab.GetComponent<Bullet>();
        bullet.SetBulletType(BulletType.Player);
    }
    void Update()
    {
        // プレイヤーの向きをポインターに合わせて変更する
        SetRadian(GetRadian());
        
        if (Input.GetKey(KeyCode.Z))
        {
            _timer += Time.deltaTime;            
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            // レーザー
            if (_timer > 1f)
            {
                _timer = 0;
                Instantiate(_laserBulletPrefab, this.transform.position, _player.transform.rotation);
                Debug.Log("レーザー発射");
            }
            // 単発
            else
            {
                var bulletObj = Instantiate(_bulletPrefab, this.transform.position, _player.transform.rotation);
                var bullet = bulletObj.GetComponent<Bullet>();
                // 弾の飛ぶ方向(力の向き)をプレイヤーの向きに合わせる
                bullet.SetDirection(_player.transform.right);
                Debug.Log("単発発射");
            }
            _timer = 0;
        }
        
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            // 射出角度はプレイヤーの向きに合わせる
            var bulletObj = Instantiate(_bulletPrefab, this.transform.position, _player.transform.rotation);
            var bullet = bulletObj.GetComponent<Bullet>();
            // 弾の飛ぶ方向(力の向き)をプレイヤーの向きに合わせる
            bullet.SetDirection(_player.transform.right);
        }*/
    }

    /// <summary>
    /// ポインターに対する角度を取得する(銃口から)
    /// </summary>
    /// <returns></returns>
    private float GetRadian()
    {
        var targetPos = _target.transform.position;
        var direction = targetPos - this.transform.position;
        _radian = Mathf.Atan2(direction.y, direction.x);  // yが先
        return _radian;
    }

    /// <summary>
    /// プレイヤーの角度を変更する
    /// </summary>
    /// <param name="angle"></param>
    public void SetRadian(float angle)
    {
        _radian = angle;
        var rotation = Quaternion.AngleAxis(_radian * 180 / Mathf.PI, new Vector3(0, 0, 1));
        _player.transform.rotation = rotation;
        
        // 銃口の角度を変えたい場合。Update内も変更する必要あり
        //this.transform.rotation = rotation;
    }
}
