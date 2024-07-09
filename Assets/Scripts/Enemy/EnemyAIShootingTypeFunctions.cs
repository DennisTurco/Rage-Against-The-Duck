using UnityEngine;

public class EnemyAIShootingTypeFunctions : MonoBehaviour
{
    public void SimpleShooting(GameObject bulletPrefab, Vector2 pos)
    {
        // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
        GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        newBullet.name = "EnemyBullet";

        // Calcola l'angolo di rotazione della munizione basato sulla direzione di sparo
        Vector2 direction = pos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void DiagonalShooting(GameObject bulletPrefab)
    {
        for (int i = 0; i < 4; ++i)
        {
            // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            newBullet.name = "EnemyBullet";


            // Calculate the angle of rotation of the ammunition based on the firing direction
            if (i == 0)
            {
                // shoting top left
                float angle = Mathf.Atan2(Vector2.up.x + Vector2.left.x, Vector2.up.y + Vector2.left.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 1)
            {
                // shoting top right
                float angle = Mathf.Atan2(Vector2.up.x + Vector2.right.x, Vector2.up.y + Vector2.right.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 2)
            {
                // shoting bottom left
                float angle = Mathf.Atan2(Vector2.down.x + Vector2.left.x, Vector2.down.y + Vector2.left.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 3)
            {
                // shoting bottom right
                float angle = Mathf.Atan2(Vector2.down.x + Vector2.right.x, Vector2.down.y + Vector2.right.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

        }
    }

    public void FourAxesShooting(GameObject bulletPrefab)
    {
        for (int i = 0; i < 4; ++i)
        {
            // Istanzia la munizione nella posizione del firePoint e nella direzione di sparo
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            newBullet.name = "EnemyBullet";


            // Calculate the angle of rotation of the ammunition based on the firing direction
            if (i == 0)
            {
                // shoting up
                float angle = Mathf.Atan2(Vector2.up.x, Vector2.up.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 1)
            {
                // shoting down
                float angle = Mathf.Atan2(Vector2.down.x, Vector2.down.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 2)
            {
                // shoting left
                float angle = Mathf.Atan2(Vector2.left.x, Vector2.left.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else if (i == 3)
            {
                // shoting right
                float angle = Mathf.Atan2(Vector2.right.x, Vector2.right.y) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

        }
    }

    public void ShotgunShooting(GameObject bulletPrefab, Vector2 pos)
    {
        int numBullets = 3;
        float spreadAngle = 15f; // Angolo di separazione tra i proiettili

        // Calcola la direzione principale di sparo
        Vector2 direction = pos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - spreadAngle * (numBullets - 1) / 2;

        for (int i = 0; i < numBullets; ++i)
        {
            // Istanzia la munizione nella posizione del firePoint
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            newBullet.name = "EnemyBullet";

            // Calcola l'angolo di rotazione della munizione
            float angle = startAngle + spreadAngle * i;
            newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void RandomShotgunShooting(GameObject bulletPrefab, Vector2 pos)
    {
        // Numero di proiettili random tra 2 e 3
        int numBullets = Random.Range(2, 4);
        float spreadAngle = 15f; // Angolo di separazione tra i proiettili

        // Calcola la direzione principale di sparo
        Vector2 direction = pos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - spreadAngle * (numBullets - 1) / 2;

        for (int i = 0; i < numBullets; ++i)
        {
            // Istanzia la munizione nella posizione del firePoint
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            newBullet.name = "EnemyBullet";

            // Calcola l'angolo di rotazione della munizione con un offset randomico
            float angle = startAngle + spreadAngle * i + Random.Range(-spreadAngle / 2, spreadAngle / 2);
            newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
