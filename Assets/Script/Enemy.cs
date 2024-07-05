using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject _bulletPrefab;

    [SerializeField, Tooltip("�����蔻��͈̔�(����)")]
    private float _radius = 9.0f;

    [SerializeField, Tooltip("���̑���(�}�C�i�X�ɂ��邱�Ƃō��ɔ��)")]
    private float _bulletSpeed = -5.0f;

    private Vector2 _vDistance;
    private float _distance;
    private float _timer = 0.0f;
    private float _interval = 1.0f;


    // Start is called before the first frame update
    /*void Start()
    {
        // BulletGenerator��Start�ɂ��̏����������Ă��邽��
        // ���Ƃ��ɃX�e�[�g��ύX����K�v������(�t���R��)
        // �����FBullet�̕��ŃX�e�[�g�ɍ��킹�ăI�u�W�F�N�g���̂�ς����ق�����������
        var bullet = _bulletPrefab.GetComponent<Bullet>();
        bullet.Type = BulletType.Enemy;
        bullet.Speed = _bulletSpeed;
    }*/

    private void Update()
    {
        _vDistance = _player.transform.localPosition - this.transform.position;
        _distance = _vDistance.x * _vDistance.x + _vDistance.y * _vDistance.y;   
        // Player�̒��S���~�̒��ɓ����Ă����
        if (_distance <= _radius * _radius)
        {
            _timer += Time.deltaTime;
            ShootBullet();
            //Debug.Log($"{this.name}�͈͓�");
        }
        
    }

    /// <summary>
    /// �������i���̂Ƃ��떳���ɏo��̂Œ��Ӂj
    /// </summary>
    private void ShootBullet()
    {
        // ���ˊԊu��1�b������
        if (_timer > _interval)
        {
            // ��[����o������ x - 0.5
            var obj = Instantiate(_bulletPrefab, this.transform.position + new Vector3(-0.5f, 0, 0), this.transform.rotation);
            
            // ����G�d�l�ɕύX����
            var bullet = obj.GetComponent<Bullet>();
            bullet.Type = BulletType.Enemy;
            bullet.Speed = _bulletSpeed;

            _timer = 0.0f;
        }
    }
}
