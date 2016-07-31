using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    Transform waypoint;
    int wayPointIndex = 0;


    void GetNextWaypoint()
    {
        if (wayPointIndex + 1 >= Waypoints.nodes.Length)
        {
            return;
        }
        waypoint = Waypoints.nodes[wayPointIndex];
        wayPointIndex++;
    }
	
	// Update is called once per frame
	void Update () {
        if (null == waypoint) {
            GetNextWaypoint();
            if (null == waypoint) {
                ReachedGoal();
                return;
            }
        }

        // Move
        Vector3 direction = waypoint.position - this.transform.position;
        float distanceInThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceInThisFrame) {
            waypoint = null;
        } else {
            transform.Translate(direction.normalized * distanceInThisFrame, Space.World);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
	}

    void ReachedGoal()
    {
        Destroy(gameObject);
    }
}
