using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DriverAgent : Agent
{
    public GameObject vehicle;
    private ForwardView forwardView;
    private VehicleControl control;
    private Vector3 lastPosition = new Vector3(5.3f,0.95f,-296.46f);

    public GameObject FinishTrigger;

    float laptime = 0f;
    float distance = 0f;

    void Start()
    {
        if(vehicle!=null)
        {
            forwardView = vehicle.GetComponent<ForwardView>();
            control = vehicle.GetComponent<VehicleControl>();
        }
    }

    public void FixedUpdate()
    {
        laptime += Time.deltaTime;
        //RequestDecision();
    }

    public override void OnEpisodeBegin()
    {
        int x = Random.Range(0,2);
        var rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        laptime = 0f;
        distance = 0f;

        if (x == 0)
        {
            this.transform.position = new Vector3(5.30000019f,0.949999988f,-296.459991f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f,-90f,0f));
        }
        if (x == 1)
        {
            this.transform.position = new Vector3(1f,0.949999988f,-156.100006f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f,92.5582581f,0f));
        }
        if (x == 2)
        {
            this.transform.position = new Vector3(187f,0.949999988f,-261.600006f);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f,214.119965f,0f));
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float acc = Mathf.Clamp(actions.ContinuousActions[0], 0f, 1f);
        float steer = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        int brake = actions.DiscreteActions[0];

        Debug.Log(acc);

        //forward the values to vehicle
        control.agentaccel = acc;
        control.agentsteer = steer;
        control.agentbrake = brake;

        if (forwardView.OnRoad == false)
        {
            AddReward(distance / laptime);
            EndEpisode();
        }

        Vector3 currposition = this.transform.position;
        if (Vector3.Distance(currposition, lastPosition)>=1)
        {
            distance += 1f;
            lastPosition = currposition;
            AddReward(0.5f*control.speed);
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
            AddReward(distance / laptime);
        }
    }
}
