using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    public float range = 1f;
    public GameObject rotatingPart;

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
