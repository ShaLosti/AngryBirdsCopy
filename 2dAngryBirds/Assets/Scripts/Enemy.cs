using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy>()!=null) return;
        Bird plrbird = collision.collider.GetComponent<Bird>();
        if (plrbird != null)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            return;
        }
        if (collision.contacts[0].normal.y < -.5f)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
