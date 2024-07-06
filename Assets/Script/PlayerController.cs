using UnityEngine;

public class PlayerController : Life
{
    [SerializeField]
    private float _jumpPower = 3.0f;

    [SerializeField, Tooltip("必ず正の値を入れる")]
    private float _gravity = 30.0f;

    private float _speed = 5.0f;
    private float _jumpElapsedTime = 0.0f;
    private float _offsetY;           // 初期の高さ
    private bool _isGround = true;    // 接地判定用
    private Vector3 pos;

    //private int _JumpCount = 2;

    private void Start()
    {
        // ゲームマネージャにプレイヤーを登録
        GameManager.Instance.Register(this);
        pos = transform.position;
        _offsetY = pos.y;
    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();

        if (Hp <= 0)
        {
            //Debug.Log("GameOver");
        }
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