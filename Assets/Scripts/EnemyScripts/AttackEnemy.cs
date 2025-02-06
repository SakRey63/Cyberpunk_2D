using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private EnemyWeapon _enemyWeapon;
    
    private float _delay = 0.7f;
    private bool _isAttack = true;

    public bool IsAttack => _isAttack;
    
    private IEnumerator Assault(int damage)
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _enemyWeapon.gameObject.SetActive(true);

        _isAttack = false;
        
        yield return delay;

        _isAttack = true;
        
        if (_enemyWeapon.CountPlayers > 0) 
        { 
            _enemyWeapon.GetPlayers(damage);
        }
        
        _enemyWeapon.gameObject.SetActive(false);
    }
    
    public void Attack(int damage)
    {
        StartCoroutine(Assault(damage));
    }
}
