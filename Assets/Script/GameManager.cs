using System.Collections.Generic;

public class GameManager
{
    static GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    private GameManager() { }
    

    private PlayerController _player;
    private List<Enemy> _enemies = new List<Enemy>();

    public PlayerController Player => _player;
    public List<Enemy> Enemies => _enemies;

    // Œ^‚²‚Æ‚É“o˜^‚·‚é
    public void Register(PlayerController player) { _player = player; }
    public void Register(Enemy enemy) {  _enemies.Add(enemy); }
    public void Unregister(Enemy enemy) { _enemies.Remove(enemy); }
}
