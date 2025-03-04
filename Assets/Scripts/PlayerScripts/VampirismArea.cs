using System.Collections.Generic;
using UnityEngine;

public class VampirismArea : MonoBehaviour
{
    private Enemy _enemy;
    
    private List<Enemy> _enemies;

    public int Index => _enemies.Count;
    
    private void OnEnable()
    {
        _enemies = new List<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy)) 
        {
            _enemies.Add(enemy);
        }
    }
         
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        { 
            _enemies.Remove(enemy); 
        }
    }
    
    private float GetDirection(Enemy enemy)
    {
        Vector3 playerPosition = gameObject.transform.position;
        
        float tempDistance = (enemy.transform.position - playerPosition).sqrMagnitude;
        
        return tempDistance;
    }
    
    public Enemy SelectTarget()
    {
        float tempDistance;
        
        if (_enemies.Count > 0)
        {
            _enemy = _enemies[0];
            
            float distance = GetDirection(_enemies[0]);
            
            foreach (Enemy enemy in _enemies)
            {
                tempDistance = GetDirection(enemy);
                
                if (tempDistance < distance)
                {
                    distance = tempDistance;

                    _enemy = enemy;
                }
            }
        }

        return _enemy;
    }
}
