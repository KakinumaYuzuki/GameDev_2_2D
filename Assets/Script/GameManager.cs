using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    private GameManager() { }
    

    private PlayerController _player;
    private List<Enemy> _enemies = new List<Enemy>();
    private List<Bullet> _bullets = new List<Bullet>();

    private float _timer = 0;

    public PlayerController Player => _player;
    public List<Enemy> Enemies => _enemies;
    public List<Bullet> Bullets => _bullets;

    // 型ごとに登録する
    public void Register(PlayerController player) { _player = player; }
    public void Register(Enemy enemy) { _enemies.Add(enemy); }
    public void Register(Bullet bullet) { _bullets.Add(bullet); }
    public void Unregister(Enemy enemy) { _enemies.Remove(enemy); }
    public void Unregister(Bullet bullet) { _bullets.Remove(bullet); }
    
    /// <summary>
    /// ヒットストップ
    /// </summary>
    /// <param name="time">止める時間</param>
    /// <remarks>プレイヤーの弾が当たった時に呼ぶ</remarks>
    public void HitStop(float time)
    {
        _timer = time;
        //Debug.Log("Hit");
        Time.timeScale = 0;
    }

    /// <summary>
    /// ヒットストップの解除
    /// タイムスケールを元に戻す
    /// </summary>
    /// <remarks>プレイヤーから呼んでおく</remarks>
    public void SwitchTimeScale()
    {
        // タイムスケールが1(通常)の時は何もしない
        if (Time.timeScale > 0)
        {
            return;
        }
        
        _timer -= Time.unscaledDeltaTime;
        if (_timer < 0)
        {
            Time.timeScale = 1;
        }
    }
}
