using UnityEngine;
using UnityEngine.UI;


namespace Unity.MLAgents.Demonstrations
{
    public class TORTimer : MonoBehaviour
    {

        public Text countdownText;
        private float countdownTimer = 3.0f;
        private bool countdown = false;

        private int timerDuration;

        public Image panelImage;
        private Color originalColor;

        public VehicleControl control;
        public DemonstrationRecorder car;
        public WaypointPos roadPos;
        public WaypointPos roadPos2;
        public WaypointPos roadPos3;
        public WaypointPos roadPos4;
        public WaypointPos roadPos5;

        public AudioClip soundClip; 

        private AudioSource audioSource;

        void Start()
        {
            panelImage.color = new Color(11f / 255f, 110f / 255f, 79f / 255f);
            
            // Get the AudioSource component attached to the GameObject
            audioSource = GetComponent<AudioSource>();
            // Set the audio clip for the AudioSource
            audioSource.clip = soundClip;
            BeginEp();
        }

        void OnCountdownBegin()
        {
            countdown = true;
            panelImage.color = new Color(204f / 255f, 41f / 255f, 54f / 255f);
        }

        void OnTimerEnd()
        {
            //Debug.Log("Timer has ended.");
            audioSource.Play();
            panelImage.color = new Color(204f / 255f, 41f / 255f, 54f / 255f);
            countdownText.text = "Take over!";
            control.autodrive = false;
            car.Record = true;

        }

        public void BeginEp()
        {
            timerDuration = Random.Range(8, 15);
            //Invoke("OnCountdownBegin", timerDuration - 3);
            Invoke("OnTimerEnd", timerDuration);
        }

        public void EndEp()
        {
            car.Record = false;
            panelImage.color = new Color(11f / 255f, 110f / 255f, 79f / 255f);
            countdownText.text = "Autonomous Driving";
            roadPos.ReGen();
            roadPos2.ReGen();
            roadPos3.ReGen();
            roadPos4.ReGen();
            roadPos5.ReGen();
            BeginEp();
            //control.autodrive = true;
        }

        void FixedUpdate()
        {
            // if (countdown)
            // {
            //     if (countdownTimer > 0)
            //     {
            //         countdownTimer -= Time.deltaTime;
            //         countdownText.text = "Time left to take over: " + Mathf.Round(countdownTimer);
            //     }
            //     else
            //     {
            //         countdownText.text = "Take over!";
            //         countdown = false;
            //         countdownTimer = 3.0f;
            //     }
            // }
        }
    }
}

