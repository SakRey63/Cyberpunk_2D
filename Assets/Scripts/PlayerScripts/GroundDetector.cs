using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radiusDetector = 0.6f;
    private bool _isGround;
    private Collider2D[] _result = new Collider2D[5];
    
    public bool ScanSoil()
    {
        _isGround = false;
        
        int numberHits = Physics2D.OverlapCircleNonAlloc(transform.position, _radiusDetector, _result);

        for (int i = 0; i < numberHits; i ++)
        {
            if (_result[i].TryGetComponent<Platform>(out _))
            {
                _isGround = true;
                
                break;
            }
        }
        
        return _isGround;
    }
}
