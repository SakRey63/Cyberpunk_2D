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
    [SerializeField] private MedicineChest _medicineChest;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private float _delaySpawnMoney = 1.5f;
    [SerializeField] private float _delaySpawrMedicineChest = 10f;

    private ObjectPool<Money> _poolMoney;
    private ObjectPool<MedicineChest> _poolMedicineChest;

    private List<Transform[]> _transforms;
    
    private void Awake()
    {
        _transforms = new List<Transform[]>()
        {
            _skateboarderPatrolPoints,
            _tankPatrolPoints,
            _baseballPlayerPatrolPoints
        };
             
        _poolMoney = new ObjectPool<Money>(
            createFunc: () => Instantiate(_money),
            actionOnGet: (money) => GetAction(money.gameObject),
            actionOnRelease: (money) => money.gameObject.SetActive(false),
            actionOnDestroy: (money) => Destroy(money),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
        
        _poolMedicineChest = new ObjectPool<MedicineChest>(
            createFunc: () => Instantiate(_medicineChest),
            actionOnGet: (medicineChest) => GetAction(medicineChest.gameObject),
            actionOnRelease: (medicineChest) => medicineChest.gameObject.SetActive(false),
            actionOnDestroy: (medicineChest) => Destroy(medicineChest),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnMoney());
        StartCoroutine(SpawnMedicineChest());
    }

    private IEnumerator SpawnMoney()
    {
        WaitForSeconds delay = new WaitForSeconds(_delaySpawnMoney);

        while (enabled)
        {
            _poolMoney.Get();

            yield return delay;
        }
    }
    
    private IEnumerator SpawnMedicineChest()
    {
        WaitForSeconds delay = new WaitForSeconds(_delaySpawrMedicineChest);

        while (enabled)
        {
            _poolMedicineChest.Get();

            yield return delay;
        }
    }

    private void GetAction(GameObject obj)
    {
        if (obj.TryGetComponent(out Money money))
        {
            money.WasDiscovered += ReleaseMoney;
        }
        
        obj.transform.position = RandomPositionMoney(GetPlatform());
        obj.gameObject.SetActive(true);
    }

    private void ReleaseMoney(Money money)
    {
        money.WasDiscovered -= ReleaseMoney;
        
        _poolMoney.Release(money);
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
        
        return new Vector2(numberPointX, numberPointY);
    }
}
