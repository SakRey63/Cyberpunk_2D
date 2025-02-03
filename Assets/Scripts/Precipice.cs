using UnityEngine;

public class Precipice : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _) || other.gameObject.TryGetComponent(out Enemy _))
        {
            Destroy(other.gameObject);
        }
    }
}
