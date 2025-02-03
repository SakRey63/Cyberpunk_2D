using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _tankPatrolPoints;
    [SerializeField] private Transform[] _baseballPlayerPatrolPoints;
    [SerializeField] private Transform[] _skateboarderPatrolPoints;
    [SerializeField] private Money _money;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10;
    [SerializeField] private float _delay = 1.5f;

    private ObjectPool<Money> _pool;

    private List<Transform[]> _transforms;
    
    private void Awake()
    {
        _transforms = new List<Transform[]>()
        {
            _skateboarderPatrolPoints,
            _tankPatrolPoints,
            _baseballPlayerPatrolPoints
        };
             
        _pool = new ObjectPool<Money>(
            createFunc: () => Instantiate(_money),
            actionOnGet: (money) => GetAction(money),
            actionOnRelease: (money) => money.gameObject.SetActive(false),
            actionOnDestroy: (money) => Destroy(money),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnMoney());
    }

    private IEnumerator SpawnMoney()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            _pool.Get();

            yield return delay;
        }
    }

    private void GetAction(Money money)
    {
        money.FoundMe += ReleaseMoney;
        
        money.transform.position = RandomPositionMoney(GetPlatform());
        money.gameObject.SetActive(true);
    }

    private void ReleaseMoney(Money money)
    {
        money.FoundMe -= ReleaseMoney;
        
        _pool.Release(money);
    }
    private Transform[] GetPlatform()
    {
        int index = Random.Range(0, _transforms.Count);

        return _transforms[index];
    }

    private Vector2 RandomPositionMoney(Transform[] points)
    {
        float numberPointX = Random.Range(points[0].transform.position.x, points[points.Length - 1].transform.position.x);
        float numberPointY = points[0].transform.position.y;

        Vector2 point = new Vector2(numberPointX, numberPointY);
        
        return point;
    }
}
