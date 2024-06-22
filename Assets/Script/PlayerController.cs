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

    // 接地判定用
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
            pos.y = Mathf.Sqrt(_jumpPower * -2f * _gravity); // ジャンプの初速度を計算
            _isGround = false;
        }
        if (!_isGround)
        {
            pos.y += _gravity * Time.deltaTime; // 重力を加算
            transform.position += new Vector3(0, pos.y * Time.deltaTime, 0); // 垂直方向の位置を更新
            if (transform.position.y < _offsetY)
            {
                _isGround = true;
            }
        }
    }
}

