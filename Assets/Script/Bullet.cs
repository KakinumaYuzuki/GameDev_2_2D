using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _pos;
    private float _speed = 5.0f;

    private void Start()
    {
        _pos = transform.position;
    }

    private void Update()
    {
        _pos.x += _speed * Time.deltaTime;
        transform.position = _pos;
    }
}
