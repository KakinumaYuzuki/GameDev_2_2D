using UnityEngine;

/// <summary>
/// スポーン判定
/// スポーン地点1つにつき1オブジェクトを生成する
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnInterval = 1f;
    
    private float _timer = 0;
    private GameObject _obj = null;
    
    void Start()
    {
        // 初期は生成状態にする
        if (_obj == null)
        {
            _obj = Instantiate(_prefab, this.transform);
        }
    }
    
    void Update()
    {
        Spawn();
    }

    /// <summary>
    /// 自分から生成されたオブジェクトがあるか調べる
    /// </summary>
    /// <returns>bool オブジェクトがあればtrue、そうでなければfalse</returns>
    private bool ExistenceCheck()
    {
        if (_obj != null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 生成間隔を測る
    /// </summary>
    /// <returns>bool 生成時間に達すればtrue、そうでなければfalse</returns>
    private bool IntervalCheck()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnInterval)
        {
            _timer = 0;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 自分から生成されたオブジェクトがないとき
    /// 生成時間を超えるとオブジェクトを生成する
    /// </summary>
    private void Spawn()
    {
        if (!ExistenceCheck())
        {
            if (IntervalCheck())
            {
                _obj = Instantiate(_prefab, this.transform.position, Quaternion.identity);
            }
        }
    }
}