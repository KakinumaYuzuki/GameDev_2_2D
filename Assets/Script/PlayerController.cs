using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5.0f;
    Vector3 pos;

    [SerializeField]
    private float _jumpPower = 3f;
    [SerializeField]
    private float _gravity = -9.81f;

    //private int _JumpCount = 2;

    private float _offsetY;

    // �ڒn����p
    private bool _isGround = true;

    private void Start()
    {
        pos = transform.position;
        _offsetY = pos.y;
    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontal * Time.deltaTime * _speed, 0, 0);
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            pos.y = Mathf.Sqrt(_jumpPower * -2f * _gravity); // �W�����v�̏����x���v�Z
            _isGround = false;
        }
        if (!_isGround)
        {
            pos.y += _gravity * Time.deltaTime; // �d�͂����Z
            transform.position += new Vector3(0, pos.y * Time.deltaTime, 0); // ���������̈ʒu���X�V
            if (transform.position.y < _offsetY)
            {
                _isGround = true;
            }
        }
    }
}

