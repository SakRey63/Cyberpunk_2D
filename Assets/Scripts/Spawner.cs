using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private MedicineChest _medicineChest;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private float _delaySpawnMoney = 2f;
    [SerializeField] private float _delaySpawrMedicineChest = 15f;
    
    private ObjectPool<Money> _poolMoney;
    private ObjectPool<MedicineChest> _poolMedicineChest;

    private void Awake()
    {
        _poolMoney = new ObjectPool<Money>(
            createFunc: () => Instantiate(_money),
            actionOnGet: money => GetAction(money.gameObject),
            actionOnRelease: money => money.gameObject.SetActive(false),
            actionOnDestroy: money => Destroy(money),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _poolMedicineChest = new ObjectPool<MedicineChest>(
            createFunc: () => Instantiate(_medicineChest),
            actionOnGet: medicineChest => GetAction(medicineChest.gameObject),
            actionOnRelease: medicineChest => medicineChest.gameObject.SetActive(false),
            actionOnDestroy: medicineChest => Destroy(medicineChest),
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

        if (obj.TryGetComponent(out MedicineChest medicine))
        {
            medicine.WasApplied += ReleaseMedicineChest;
        }
        
        obj.transform.position = _spawnPoints.RandomPosition();
        obj.gameObject.SetActive(true);
    }

    private void ReleaseMedicineChest(MedicineChest medicine)
    {
        medicine.WasApplied -= ReleaseMedicineChest;
        
        _poolMedicineChest.Release(medicine);
    }
    
    private void ReleaseMoney(Money money)
    {
        money.WasDiscovered -= ReleaseMoney;
        
        _poolMoney.Release(money);
    }
}
