using System.Collections.Generic;

public class GameManager
{
    static GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    private GameManager() { }
    

    private PlayerController _player;
    private List<Enemy> _enemies = new List<Enemy>();
    private List<Bullet> _bullets = new List<Bullet>();

    public PlayerController Player => _player;
    public List<Enemy> Enemies => _enemies;
    public List<Bullet> Bullets => _bullets;

    // 型ごとに登録する
    public void Register(PlayerController player) { _player = player; }
    public void Register(Enemy enemy) { _enemies.Add(enemy); }
    public void Register(Bullet bullet) { _bullets.Add(bullet); }
    public void Unregister(Enemy enemy) { _enemies.Remove(enemy); }
    public void Unregister(Bullet bullet) { _bullets.Remove(bullet); }
}
