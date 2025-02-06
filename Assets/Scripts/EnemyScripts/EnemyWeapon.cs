using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int CountPlayers => _players.Count;

    public event Action<Player, int> IsAttack;

    private List<Player> _players;

    private void OnEnable()
    {
        _players = new List<Player>();
    }

    private void OnDisable()
    {
        _players.Clear();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _players.Add(player);
        }
    }

    public void GetPlayers(int damage)
    {
        IsAttack?.Invoke(_players[0], damage);
    }
}
