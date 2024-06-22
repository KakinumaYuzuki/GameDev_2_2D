using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRigidbody : MonoBehaviour
{
    [SerializeField]
    float _gravity = -1f;

    [SerializeField]
    float _maxAcceleration = 30;
    public Vector3 _velocity;

    float _offsetY;
    //public float OffsetY { get => _offsetY; set => _offsetY = value; }
    //public float OffsetY { get => _offsetY; set => _offsetY = value; }

    // Ú’n”»’è—p
    public bool IsGround { get{ return transform.position.y <= _offsetY; } } // 

    // Start is called before the first frame update
    void Start()
    {
        _offsetY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_velocity.y > 0 || transform.position.y != _offsetY)
        {
            //if (Mathf.Abs(_velocity.y) <= _maxAcceleration)
            //{
                _velocity.y += _gravity;
            //}
            Debug.Log("a");
            if (transform.position.y < _offsetY)
            {
                transform.position = new Vector3(transform.position.x, _offsetY);
                _velocity.y = 0;
                Debug.Log("b");
            }
        }

        transform.position += _velocity * Time.deltaTime;
    }

    public void SelfAddForce(Vector3 power)
    {
        _velocity += power;
    }
}
