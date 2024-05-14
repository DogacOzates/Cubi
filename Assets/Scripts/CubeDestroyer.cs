using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public float destroyDistance = 1.1f; // Yok edilecek küplerin maksimum mesafesi

    void OnMouseDown()
    {
        DestroyCubeAndNeighbors();
    }

    void OnTouchDown()
    {
        DestroyCubeAndNeighbors();
    }

    // Seçilen küp ve bitişik olan küpleri yok eden fonksiyon
    void DestroyCubeAndNeighbors()
    {
        // Seçilen küpü yok et
        Destroy(gameObject);

        // Bitişik küpleri bul ve yok et
       Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(destroyDistance, destroyDistance, destroyDistance));
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject && hitCollider.CompareTag("Cube") && Vector3.Distance(hitCollider.transform.position, transform.position) <= destroyDistance)
            {
                Destroy(hitCollider.gameObject);
            }
        }
    }
}
