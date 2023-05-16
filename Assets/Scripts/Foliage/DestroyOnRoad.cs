using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnRoad : MonoBehaviour
{
    [SerializeField]
    LayerMask roadLayer;

    private void FixedUpdate()
    {
        if(Physics.Raycast(gameObject.transform.position, Vector3.down, 50, roadLayer))
        {
            Destroy(gameObject);
        }
    }
}