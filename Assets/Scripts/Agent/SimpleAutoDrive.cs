using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class SimpleAutoDrive : MonoBehaviour
    {


        public VehicleControl control;

        public PathCreator road;

        public float maxSpeed = 5.0f;
        public float speed = 5;

        public GameObject targetGameObject = null;
        public Vector3 target = Vector3.zero;


        void FixedUpdate()
        {

            if (targetGameObject != null)
            {

                Vector3 normalizedDirection = targetGameObject.transform.position - transform.position;

                float whichWay = Vector3.Cross(transform.forward, normalizedDirection).y;

                print(whichWay);
                //target = targetGameObject.transform.position - transform.position;

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
                    control.agentsteer = 0.0f;
                }

                //acceleration
                if (control.speed <= 30.0f)
                {
                    control.agentaccel = 1.0f;
                }
                if (control.speed > 30.0f)
                {
                    control.agentaccel = 0.0f;
                }
            }

        }
    }
}

