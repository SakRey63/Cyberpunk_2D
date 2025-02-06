using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayer : MonoBehaviour
{
    public int CountEnemies => _enemies.Count;

    public event Action<Enemy, int> IsAttack;

    private List<Enemy> _enemies;

    private void OnEnable()
    {
        _enemies = new List<Enemy>();
    }

    private void OnDisable()
    {
        _enemies.Clear();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
        }
    }

    public void GetEnemy(int damage)
    {
        IsAttack?.Invoke(_enemies[0], damage);
    }
}
