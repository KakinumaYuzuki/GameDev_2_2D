using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField]
    private float _appear = 0.2f;
    //[SerializeField]
    //private float _hitTime = 0.1f; // サンプル画像にあったけど使ってない
    [SerializeField]
    private Transform _hit;
    
    private float _timer;
    private int _damageValue = 3;
    
    void Update()
    {
        _timer += Time.deltaTime;
        // 出現経過時間が一定の値を超えると自身を破棄する
        if (_timer > _appear)
        {
            Destroy(this.gameObject);
        }
        DealDamage();
    }
    
    /// <summary>
    /// 当たり判定をとる 線分と矩形の判定
    /// </summary>
    /// <param name="target">Transform 弾に当たる判定の対象</param>
    /// <returns>弾に当たっていればtrue、そうでなければfalse</returns>
    /// <remarks>スケールで端を調整しているので対象が回転している場合に対応していない</remarks>
    private bool IsHit(Transform target)
    {
        var pos = _hit.position;
        var halfScale = _hit.localScale / 2;
        var targetPos = target.position;
        var targetHalfScale = target.localScale / 2;
        
        // 判定対象の矩形の角の頂点
        var cornerPositions = new Vector2[]
        {
            new Vector2(targetPos.x - targetHalfScale.x, targetPos.y - targetHalfScale.y),  // 左下
            new Vector2(targetPos.x - targetHalfScale.x, targetPos.y + targetHalfScale.y),  // 左上
            new Vector2(targetPos.x + targetHalfScale.x, targetPos.y + targetHalfScale.y),  // 右下
            new Vector2(targetPos.x + targetHalfScale.x, targetPos.y - targetHalfScale.y)   // 右上
        };

        // 判定対象の矩形の4辺の線分(定義しなくてもいい)
        var rectangleEdges = new (Vector2, Vector2)[]
        {
            (cornerPositions[0], cornerPositions[1]),   // 左辺
            (cornerPositions[1], cornerPositions[2]),   // 上辺
            (cornerPositions[2], cornerPositions[3]),   // 右辺
            (cornerPositions[3], cornerPositions[0])    // 下辺
        };

        var lineStart = pos - _hit.right * halfScale.x; // 線分(レーザー)の始点
        var lineEnd = pos + _hit.right * halfScale.x;   // 線分(レーザー)の終点

        // 線分同士が交差しているか判定
        foreach(var edge in rectangleEdges)
        {
            if (IsLineSegmentsIntersect(lineStart, lineEnd, edge.Item1, edge.Item2))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 2つの線分が交差するかを判定する(レーザーと矩形の各辺)
    /// </summary>
    /// <param name="lineStart">線分1(レーザー)の始点</param>
    /// <param name="lineEnd">線分1(レーザー)の終点</param>
    /// <param name="edgeStart">線分2(矩形の辺)の始点</param>
    /// <param name="edgeEnd">線分2(矩形の辺)の終点</param>
    /// <returns>交差していればtrue、そうでなければfalse</returns>
    private bool IsLineSegmentsIntersect(Vector2 lineStart, Vector2 lineEnd, Vector2 edgeStart, Vector2 edgeEnd)
    {
        Vector2 v1 = lineEnd - lineStart;   // レーザーのベクトル
        Vector2 v2 = edgeEnd - edgeStart;   // 矩形の辺のベクトル

        float closs1 = Vector3.Cross(v1, edgeStart - lineStart).z;  // レーザー×レーザーの始点から辺の始点まで
        float closs2 = Vector3.Cross(v1, edgeEnd - lineStart).z;    // レーザー×レーザーの始点から辺の終点まで
        float closs3 = Vector3.Cross(v2, lineStart - edgeStart).z;  // 辺×辺の始点からレーザーの始点まで
        float closs4 = Vector3.Cross(v2, lineEnd - edgeStart).z;    // 辺×辺の始点からレーザーの終点まで

        // closs1 と closs2 が異符号 && closs3 と closs4 が異符号のとき交差している
        return closs1 * closs2 < 0 && closs3 * closs4 < 0;
    }

    /// <summary>
    /// 判定を行い、ダメージを与える
    /// </summary>
    private void DealDamage()
    {
        var bulletList = GameManager.Instance.Bullets;
        var enemyList = GameManager.Instance.Enemies;
        
        // 弾に当たれば弾を消す
        foreach (var bullet in bulletList)
        {
            if (IsHit(bullet.transform))
            {
                bullet.DestroyBullet();
                break;
            }
        }
        // 敵に当たれば3ダメージ
        foreach (var enemy in enemyList)
        {
            if (IsHit(enemy.transform))
            {
                enemy.Damage(_damageValue);
            }
        }
    }
}

