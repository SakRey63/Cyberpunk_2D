using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(Flipper))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _observationRadius = 5.0f;
    
    private Animator _animator;
    private Patroller _patroller;
    private Flipper _flipper;
    

    private void Awake()
    {
        _flipper = GetComponent<Flipper>();
        _patroller = GetComponent<Patroller>();
    }

    private void Update()
    {
        LookAround();
    }

    private Vector2 Harassment()
    {
        Vector2 target = new ();
    
        foreach (Rigidbody2D observationObject in GetExplodableObjects())
        {
            target = observationObject.transform.position;
        }

        return target;
    }
    
    private List<Rigidbody2D> GetExplodableObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _observationRadius);

        List<Rigidbody2D> enemyes = new();

        foreach (Collider2D hit in hits)
        
            if (hit.attachedRigidbody != null && hit.TryGetComponent(out Player _))
            {
                enemyes.Add(hit.attachedRigidbody);
            }

        return enemyes;
    }

    private void LookAround()
    {
        if (GetExplodableObjects().Count == 0)
        {
            _patroller.ContinuePatrolling();
        }
        else
        {
            _flipper.PursueTarget(Harassment());
        }
    }
}
