using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public Player _player { get; private set; }
    public bool HasKeyToCastle { get; set; }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
}