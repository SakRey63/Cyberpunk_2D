using System;
using UnityEngine;

public class RefereeGame : MonoBehaviour
{
    [SerializeField] private WeaponPlayer _weaponPlayer;
    [SerializeField] private EnemyWeapon _weaponTank;
    [SerializeField] private EnemyWeapon _weaponSkateboarder;
    [SerializeField] private EnemyWeapon _weaponPBaseballPlayer;
    
    public event Action<int> ChangeHealthPlayer;
    
    private void OnEnable()
    {
        _weaponPlayer.IsAttack += EnemyDamage;
        _weaponTank.IsAttack += PlayerDamage;
        _weaponSkateboarder.IsAttack += PlayerDamage;
        _weaponPBaseballPlayer.IsAttack += PlayerDamage;
    }

    private void OnDisable()
    {
        _weaponPlayer.IsAttack -= EnemyDamage;
        _weaponTank.IsAttack -= PlayerDamage;
        _weaponSkateboarder.IsAttack -= PlayerDamage;
        _weaponPBaseballPlayer.IsAttack -= PlayerDamage;
    }

    private void EnemyDamage(Enemy enemy, int damage)
    {
        enemy.TakeDamage(damage);
    }

    private void PlayerDamage(Player player, int damage)
    {
        player.TakeDamage(damage);

        ChangeHealthPlayer?.Invoke(player.Health);
    }
}
