using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;


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

        // Reaction Timer Variables
        private float timer;
        private float startValue1;
        private float startValue2;
        private float startValue3;
        private bool isTimerRunning;

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

            // Start a timer, when the Input.GetAxis("Horizontal") has changed by 0.1, stop the timer and store the result in a CSV
            timer = 0f;
            startValue1 = Input.GetAxis("Horizontal");
            startValue2 = Input.GetAxis("Vertical");
            startValue3 = Input.GetAxis("Brake");
            isTimerRunning = true;
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
        }

        private void StoreResultToCSV(float result)
        {
            string filePath = "Assets/Reactions/result.csv";
            string delimiter = ",";

            // Check if the file exists
            bool fileExists = File.Exists(filePath);

            // Open or create the file
            StreamWriter writer = new StreamWriter(filePath, true);

            // If the file doesn't exist, write the header
            if (!fileExists)
            {
                writer.WriteLine("Reaction Time");
            }

            // Write the result to the file
            writer.WriteLine(result.ToString());

            // Close the file
            writer.Close();
        }

        private void Update()
        {
            if (isTimerRunning)
            {
                timer += Time.deltaTime;

                float currentValue1 = Input.GetAxis("Horizontal");
                float currentValue2 = Input.GetAxis("Vertical");
                float currentValue3 = Input.GetAxis("Brake");
                if (Mathf.Abs(currentValue1 - startValue1) >= 0.1f || Mathf.Abs(currentValue2 - startValue2) >= 0.1f || Mathf.Abs(currentValue3 - startValue3) >= 0.1f)
                {
                    isTimerRunning = false;
                    StoreResultToCSV(timer);
                }
            }
        }
    }
}

