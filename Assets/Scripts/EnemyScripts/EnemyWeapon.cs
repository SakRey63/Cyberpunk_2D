using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown = 1f;
    
    private bool _isReadyShoot = true;
    private bool _isHit;
    private int _healthPlayer;
    
    private void OnEnable()
    {
        _isHit = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isHit && other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
                        
            _isHit = false;
        }
    }

    public void LaunchAttack()
    {
        if (_isReadyShoot)
        {
            StartCoroutine(Recharge());
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
