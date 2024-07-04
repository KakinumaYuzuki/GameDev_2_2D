using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _jumpPower = 3.0f;

    [SerializeField, Tooltip("�K�����̒l������")]
    private float _gravity = 30.0f;

    private float _speed = 5.0f;
    private float _jumpElapsedTime = 0.0f;
    private float _offsetY;           // �����̍���
    private bool _isGround = true;    // �ڒn����p
    private Vector3 pos;

    //private int _JumpCount = 2;

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

    private void PlayerMove()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontal * Time.deltaTime * _speed, 0, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _isGround = false;
        }
        if (!_isGround)
        {
            _jumpElapsedTime += Time.deltaTime;
            pos.y = _jumpPower * _jumpElapsedTime - (_gravity * _jumpElapsedTime * _jumpElapsedTime * 0.5f) + _offsetY;
            if (transform.position.y < _offsetY)
            {
                pos.y = _offsetY;
                _isGround = true;
                _jumpElapsedTime = 0;
            }
            transform.position = new Vector3(this.transform.position.x, pos.y, this.transform.position.z);
        }
    }
}