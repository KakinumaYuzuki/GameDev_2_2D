using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_bulletPrefab, transform);
        }
    }
}
