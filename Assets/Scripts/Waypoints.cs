using UnityEngine;
using System.Collections;
using System;

public class Waypoints : MonoBehaviour
{
    public static Transform[] nodes;

	// Use this for initialization
	void Awake()
    {
        nodes = new Transform[transform.childCount];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
    }
}
