using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class SimpleAutoDrive : MonoBehaviour
    {


        public VehicleControl control;

        public PathCreator road;

        public float maxSpeed = 30.0f;
        public float speed = 25.0f;

        public GameObject targetGameObject = null;
        public Vector3 target = Vector3.zero;


        void FixedUpdate()
        {

            if (targetGameObject != null)
            {

                Vector3 normalizedDirection = targetGameObject.transform.position - transform.position;

                float whichWay = Vector3.Cross(transform.forward, normalizedDirection).y;

                //print(whichWay);

                // steering
                if (whichWay < -1.0f)
                {
                    control.agentsteer = -0.5f;
                }
                else if (whichWay > 1.0f)
                {
                    control.agentsteer = 0.5f;
                }
                else
                {
                    control.agentsteer = Mathf.Lerp(control.agentsteer, 0.0f, 0.1f);
                }

                //acceleration
                if (control.speed <= maxSpeed)
                {
                    control.agentaccel = 1.0f;
                }
                if (control.speed > maxSpeed)
                {
                    control.agentaccel = 0.0f;
                }
            }

        }
    }
}

