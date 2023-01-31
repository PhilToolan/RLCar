using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAutoDrive : MonoBehaviour
{

    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    public FollowPoints followPoints;

    public float maxForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 Calculate()
    {
        force = Vector3.zero;

        force += followPoints.Calculate();
        float f = force.magnitude;
        if (f >= maxForce)
        {
            force = Vector3.ClampMagnitude(force, maxForce);
        }

        return force;
    }

    // Update is called once per frame
    void Update()
    {
        force = Calculate();
    }
}
