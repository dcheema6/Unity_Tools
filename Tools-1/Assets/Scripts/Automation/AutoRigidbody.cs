using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Auto Rigidbody addition to object and auto assignment
    Adds a Reset item to Context Menu
*/
[RequireComponent(typeof(Rigidbody))]
public class AutoRigidbody : MonoBehaviour
{
    [HideInInspector] new public Rigidbody rigidbody;
    public float damageRadius = 1;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 0.125f);
    }

    /*
        Reset is called when you first add the component to a GameObject.
    */
    void Reset()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
