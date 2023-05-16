using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnRoad : MonoBehaviour
{
    [SerializeField]
    LayerMask roadLayer;

    private void FixedUpdate()
    {
        if(Physics.Raycast((gameObject.transform.position + new Vector3(0,1,0)), Vector3.down, 50, roadLayer))
        {
            Destroy(gameObject);
        }
    }
}