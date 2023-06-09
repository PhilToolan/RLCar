using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


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
        public GameObject rallyCar;

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

        // Positive end (reached finish line)
        public void EndEpP()
        {
            car.Record = false;
            panelImage.color = new Color(11f / 255f, 110f / 255f, 79f / 255f);
            countdownText.text = "Autonomous Driving";
            roadPos.ReGen();
            roadPos2.ReGen();
            roadPos3.ReGen();
            roadPos4.ReGen();
            roadPos5.ReGen();
            AddOutcomeToCSV("Positive");
            BeginEp();
        }

        // Negative end (didn't reach finish line)
        public void EndEpN()
        {
            car.Record = false;
            panelImage.color = new Color(11f / 255f, 110f / 255f, 79f / 255f);
            countdownText.text = "Autonomous Driving";
            roadPos.ReGen();
            roadPos2.ReGen();
            roadPos3.ReGen();
            roadPos4.ReGen();
            roadPos5.ReGen();
            AddOutcomeToCSV("Negative");
            BeginEp();
        }

        private void StoreResultToCSV(float result, string reactionMethod, float positionX, float positionZ)
        {
            string filePath = "Assets/Reactions/TestingData.csv";
            string delimiter = ",";

            // Check if the file exists
            bool fileExists = File.Exists(filePath);

            // Open or create the file
            StreamWriter writer = new StreamWriter(filePath, true);

            // If the file doesn't exist, write the header
            if (!fileExists)
            {
                writer.WriteLine("Reaction Time, Reaction Method, Car Position X, Car Position Z, Point1 X, Point1 Z, Point2 X, Point 2 Z, Point3 X, Point3 Z, Point4 X, Point4 Z, Point5 X, Point5 Z, Outcome");
            }

            // Write the data to the file
            writer.WriteLine($"{result.ToString()}{delimiter}{reactionMethod}{delimiter}{positionX}{delimiter}{positionZ}{delimiter}{roadPos.transform.position.x}{delimiter}{roadPos.transform.position.z}{delimiter}{roadPos2.transform.position.x}{delimiter}{roadPos2.transform.position.z}{delimiter}{roadPos3.transform.position.x}{delimiter}{roadPos3.transform.position.z}{delimiter}{roadPos4.transform.position.x}{delimiter}{roadPos4.transform.position.z}{delimiter}{roadPos5.transform.position.x}{delimiter}{roadPos5.transform.position.z}");

            // Close the file
            writer.Close();
        }

        private void AddOutcomeToCSV(string outcome)
        {
            string filePath = "Assets/Reactions/Outcome.csv";
            string delimiter = ",";

            // Check if the file exists
            bool fileExists = File.Exists(filePath);

            // Open or create the file
            StreamWriter writer = new StreamWriter(filePath, true);

            // If the file doesn't exist, write the header
            if (!fileExists)
            {
                writer.WriteLine("Outcome");
            }
            writer.WriteLine(outcome); // Write the outcome value

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
                if (Mathf.Abs(currentValue1 - startValue1) >= 0.1f)
                {
                    isTimerRunning = false;
                    StoreResultToCSV(timer,"Wheel", rallyCar.transform.position.x, rallyCar.transform.position.z);
                } else 
                if (Mathf.Abs(currentValue2 - startValue2) >= 0.1f)
                {
                    isTimerRunning = false;
                    StoreResultToCSV(timer, "Accelerator", rallyCar.transform.position.x, rallyCar.transform.position.z);
                } else 
                if (Mathf.Abs(currentValue3 - startValue3) >= 0.1f)
                {
                    isTimerRunning = false;
                    StoreResultToCSV(timer, "Brake", rallyCar.transform.position.x, rallyCar.transform.position.z);
                }
            }
        }
    }
}

