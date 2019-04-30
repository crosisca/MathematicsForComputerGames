using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRay : MonoBehaviour
{
    void Update()
    {
        int layerMask = 1 << 10;//cube = bit sequence 10

        //layerMask = ~layerMask;//inverte (pega td ao inves do cubo) = ~(1 << 10);

        layerMask |= 1 << 9;// mesmo que int layerMask = 1 << 10 | 1 << 9;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Missed");
        }
    }
}
