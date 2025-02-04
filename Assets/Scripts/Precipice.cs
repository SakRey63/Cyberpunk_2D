using UnityEngine;

public class Precipice : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _) || other.TryGetComponent(out Enemy _))
        {
            Destroy(other.gameObject);
        }
    }
}
