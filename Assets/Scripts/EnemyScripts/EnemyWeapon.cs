using System;
using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown = 1f;
    
    private bool _isReadyShoot = true;
    private bool _isHit;
    
    public event Action<Player, int> IsHit;
    
    public void LaunchAttack()
    {
        if (_isReadyShoot)
        {
            StartCoroutine(Recharge());
        }
    }

    private void OnEnable()
    {
        _isHit = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_isHit && other.gameObject.TryGetComponent(out Player player))
        {
            IsHit?.Invoke(player, _damage);
            
            _isHit = false;
        }
    }

    private IEnumerator Recharge()
    {
        WaitForSeconds delay = new WaitForSeconds(_cooldown);

        _isReadyShoot = false;

        yield return delay;

        _isReadyShoot = true;

        gameObject.SetActive(false);
    }
}
