using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPos : MonoBehaviour
{
    [SerializeField]
    bool wayPoint1;
    [SerializeField]
    bool wayPoint2;
    [SerializeField]
    bool wayPoint3;
    [SerializeField]
    bool wayPoint4;



    // Start is called before the first frame update
    void Start()
    {

        if (wayPoint1)
        {
            // min inclusive, max exclusive
            int xPos = Random.Range(-230, -21);
            int zPos = Random.Range(20, 151);
            this.transform.position = new Vector3(xPos, 0.1f, zPos);
        }
        else if (wayPoint2)
        {
            // min inclusive, max exclusive
            int xPos = Random.Range(-230, -16);
            int zPos = Random.Range(155, 420);
            this.transform.position = new Vector3(xPos, 0.1f, zPos);
        }
        else if (wayPoint3)
        {
            // min inclusive, max exclusive
            int xPos = Random.Range(20, 231);
            int zPos = Random.Range(300, 420);
            this.transform.position = new Vector3(xPos, 0.1f, zPos);
        }
        else if (wayPoint4)
        {
            // min inclusive, max exclusive
            int xPos = Random.Range(10, 200);
            int zPos = Random.Range(200, 300);
            this.transform.position = new Vector3(xPos, 0.1f, zPos);
        }
        else
        {
            // min inclusive, max exclusive
            int xPos = Random.Range(10, 100);
            int zPos = Random.Range(20, 151);
            this.transform.position = new Vector3(xPos, 0.1f, zPos);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            print("Regenerate road positions");

            if (wayPoint1)
            {
                // min inclusive, max exclusive
                int xPos = Random.Range(-230, -21);
                int zPos = Random.Range(20, 151);
                this.transform.position = new Vector3(xPos, 0.1f, zPos);
            }
            else if (wayPoint2)
            {
                // min inclusive, max exclusive
                int xPos = Random.Range(-230, -16);
                int zPos = Random.Range(155, 420);
                this.transform.position = new Vector3(xPos, 0.1f, zPos);
            }
            else if (wayPoint3)
            {
                // min inclusive, max exclusive
                int xPos = Random.Range(20, 231);
                int zPos = Random.Range(300, 420);
                this.transform.position = new Vector3(xPos, 0.1f, zPos);
            }
            else if (wayPoint4)
            {
                // min inclusive, max exclusive
                int xPos = Random.Range(10, 200);
                int zPos = Random.Range(200, 300);
                this.transform.position = new Vector3(xPos, 0.1f, zPos);
            }
            else
            {
                // min inclusive, max exclusive
                int xPos = Random.Range(10, 100);
                int zPos = Random.Range(20, 151);
                this.transform.position = new Vector3(xPos, 0.1f, zPos);
            }
        }
    }
}
