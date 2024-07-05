using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    private void Start()
    {
        var bullet = _bulletPrefab.GetComponent<Bullet>();
        bullet.Type = BulletType.Player;
 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_bulletPrefab, transform);
            // Enemyが球を打つときにこの処理を行っているので
            // Startでこの処理を書いてる
            /*var obj = Instantiate(_bulletPrefab, transform);
            var bullet = obj.GetComponent<Bullet>();
            bullet.Type = BulletType.Player;*/
        }
    }
}
