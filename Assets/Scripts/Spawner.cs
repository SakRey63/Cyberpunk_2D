using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item _medicine;
    [SerializeField] private Item _money;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private SpawnPoints _spawnPoints;
    [SerializeField] private float _delaySpawnItem = 5f;
    
    private ObjectPool<Item> _poolItems;

    private List<Item> _items;

    private void Awake()
    {
        _items = new List<Item>()
        {
            _money,
            _medicine
        };

        _poolItems = new ObjectPool<Item>(
            createFunc: () => Instantiate(GetRandomItem()),
            actionOnGet: item => GetAction(item),
            actionOnRelease: item => item.gameObject.SetActive(false),
            actionOnDestroy: item => Destroy(item),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        WaitForSeconds delay = new WaitForSeconds(_delaySpawnItem);

        while (enabled)
        {
            _poolItems.Get();

            yield return delay;
        }
    }
    
    private Item GetRandomItem()
    {
        return _items[Random.Range(0, _items.Count)];
    }
    
    private void GetAction(Item item)
    {
        item.Applied += ReleaseMedicineChest;
        
        item.transform.position = _spawnPoints.RandomPosition();
        item.gameObject.SetActive(true);
    }

    private void ReleaseMedicineChest(Item item)
    {
        item.Applied -= ReleaseMedicineChest;
        
        _poolItems.Release(item);
    }
}
