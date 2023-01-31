using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardView : MonoBehaviour
{

    public bool OnRoad;
    [SerializeField]
    LayerMask roadLayer;

    private void FixedUpdate()
    {
        if(Physics.Raycast(this.gameObject.transform.position, Vector3.down, 50, roadLayer))
        {
            OnRoad = true;

        }
        else OnRoad = false;
    }
}
