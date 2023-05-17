using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody targetRigidbody;   // Reference to the Rigidbody component of the object you want to track its speed
    public Text speedometerText;        // Reference to the UI Text component that will display the speed

    private void Update()
    {
        // Get the speed of the Rigidbody in meters per second
        float speedMS = targetRigidbody.velocity.magnitude;

        // Convert the speed from meters per second to kilometers per hour
        float speedKMH = speedMS * 3.6f;

        // Update the speedometer text
        speedometerText.text = string.Format("{0:0} km/h", speedKMH);
    }
}
