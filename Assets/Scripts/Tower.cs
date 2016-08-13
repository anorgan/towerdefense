using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [Header("Settings")]
    public float range = 1f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    private int timesFired = 0;

    [Header("Objects")]
    public GameObject rotatingPart;
    public GameObject missilePrefab;
    public Transform firePoint;

    GameObject currentEnemy = null;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Target", 0f, 0.5f);
	}

    void Target ()
    {
        currentEnemy = GetClosestEnemy();
    }

	// Update is called once per frame
	void Update () {
	    if (currentEnemy == null)
        {
            return;
        }

        Vector3 direction = currentEnemy.transform.position - this.transform.position;
        Vector3 rotation = Quaternion.Lerp(rotatingPart.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f).eulerAngles;

        rotatingPart.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (currentEnemy == null)
        {
            return;
        }

        Vector3 fireSlotOffset = new Vector3(timesFired * -.2f, timesFired * -.2f, 0f);

        if (timesFired < 4)
        {
            timesFired++;
        } else
        {
            timesFired = 0;
        }

        GameObject missileGO = (GameObject) Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        Missile missile = missileGO.GetComponent<Missile>();

        if (missile != null)
        {
            missile.Seek(currentEnemy.transform);
        }
    }

    GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (enemy == currentEnemy && distanceToEnemy <= range)
            {
                // Our current enemy might not be the nearest one, but is still in range
                return currentEnemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            return nearestEnemy;
        } else
        {
            return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
