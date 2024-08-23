using UnityEngine;

/// <summary>
/// ライフの管理
/// ライフを持つクラスで継承する
/// </summary>
public class Life : MonoBehaviour
{
    [SerializeField]
    private int _hp = 5;

    public int Hp => _hp;

    public void Damage(int value)
    {
        _hp -= value;
        if (_hp <= 0)
        {
            _hp = 0;
            Debug.Log($"{this.gameObject}ライフ0");
        }
    }
}
