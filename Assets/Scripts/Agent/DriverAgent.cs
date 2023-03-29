using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using PathCreation.Examples;

namespace Unity.MLAgents.Demonstrations
{
    public class DriverAgent : Agent
    {
        public GameObject vehicle;
        private ForwardView forwardView;
        private VehicleControl control;
        private Vector3 lastPosition = new Vector3(5.3f, 0.95f, -296.46f);

        public GameObject FinishTrigger;
        public TORTimer tor;
        public GeneratePathExample pathGen;

        float laptime = 0f;
        float distance = 0f;

        public DemonstrationRecorder recorder;

        void Start()
        {
            if (vehicle != null)
            {
                forwardView = vehicle.GetComponent<ForwardView>();
                control = vehicle.GetComponent<VehicleControl>();
            }
        }

        public void FixedUpdate()
        {
            laptime += Time.deltaTime;
        }

        public override void OnEpisodeBegin()
        {
            var rb = this.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            laptime = 0f;
            distance = 0f;
            control.autodrive = true;
            transform.position = new Vector3(0.0f, 0.5f, 0.0f);
            transform.rotation = Quaternion.Euler(new Vector3(0f, -45f, 0f));
            tor.BeginEp();

        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            float acc = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
            float steer = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
            int brake = actions.DiscreteActions[0];


            //forward the values to vehicle
            control.agentaccel2 = acc;
            control.agentsteer2 = steer;
            control.agentbrake2 = brake;

            if (forwardView.OnRoad == false)
            {
                //AddReward(distance / laptime);
                pathGen.GenNew();
                tor.EndEp();
                EndEpisode();
            }

            Vector3 currposition = this.transform.position;
            if (Vector3.Distance(currposition, lastPosition) >= 1)
            {
                distance += 1f;
                lastPosition = currposition;
                AddReward(0.5f * control.speed);
            }
        }

        public override void CollectObservations(VectorSensor sensor)
        {
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == FinishTrigger)
            {
                //episode completed
                //AddReward(distance / laptime);
                pathGen.GenNew();
                tor.EndEp();
                EndEpisode();
            }
        }
    }
}

