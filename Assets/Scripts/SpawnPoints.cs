using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _tankPatrolPoints;
    [SerializeField] private Transform[] _baseballPlayerPatrolPoints;
    [SerializeField] private Transform[] _skateboarderPatrolPoints;
    private List<Transform[]> _transforms;
    
    private void Awake()
    {
        _transforms = new List<Transform[]>()
        {
            _skateboarderPatrolPoints,
            _tankPatrolPoints,
            _baseballPlayerPatrolPoints
        };
    }
    
    private Transform[] GetPlatform()
    {
        int index = Random.Range(0, _transforms.Count);

        return _transforms[index];
    }

    public Vector2 RandomPosition()
    {
        Transform[] points = GetPlatform();
        
        float numberPointX = Random.Range(points[0].transform.position.x, points[points.Length - 1].transform.position.x);
        float numberPointY = points[0].transform.position.y;
        
        return new Vector2(numberPointX, numberPointY);
    }
}
