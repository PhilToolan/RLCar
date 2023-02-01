using UnityEngine;
using UnityEngine.UI;

public class TORTimer : MonoBehaviour
{

    public Text countdownText;
    private float countdownTimer = 3.0f;
    private bool countdown = false;

    private int timerDuration;

    public Image panelImage;
    private Color originalColor;

    public VehicleControl control;

    void Start()
    {
        panelImage.color = new Color(11f / 255f, 110f / 255f, 79f / 255f);

        timerDuration = Random.Range(20, 35);
        Invoke("OnCountdownBegin", timerDuration - 3);
        Invoke("OnTimerEnd", timerDuration);
    }

    void OnCountdownBegin()
    {
        countdown = true;
        panelImage.color = new Color(204f /255f, 41f / 255f, 54f / 255f);
    }

    void OnTimerEnd()
    {
        //Debug.Log("Timer has ended.");
        control.autodrive = false;

    }

    private void Update()
    {
        if (countdown)
        {
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;
                countdownText.text = "Time left to take over: " + Mathf.Round(countdownTimer);
            }
            else
            {
                countdownText.text = "Take over!";
            }
        }
    }
}
