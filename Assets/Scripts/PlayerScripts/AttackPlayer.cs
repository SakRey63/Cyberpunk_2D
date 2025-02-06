using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private WeaponPlayer _weaponPlayer;
    
    private float _delay = 0.7f;
    private bool _isAttack = true;

    public bool IsAttack => _isAttack;
    
    private IEnumerator Assault(int damage)
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _weaponPlayer.gameObject.SetActive(true);

        _isAttack = false;
        
        yield return delay;

        _isAttack = true;
        
        if (_weaponPlayer.CountEnemies > 0) 
        { 
            _weaponPlayer.GetEnemy(damage);
        }
        
        _weaponPlayer.gameObject.SetActive(false);
    }
    
    public void Attack(int damage)
    {
        StartCoroutine(Assault(damage));
    }
}
