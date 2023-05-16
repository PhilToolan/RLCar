using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject ground;
    public Collider roadCollider;
    public int numberOfObjects = 10;

    private MeshCollider meshCollider;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private void Start()
    {
 
        // Get the MeshCollider component
        meshCollider = roadCollider.GetComponent<MeshCollider>();

        // Calculate the boundaries of the cube
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        minBounds = groundRenderer.bounds.min;
        maxBounds = groundRenderer.bounds.max;

        // Spawn objects
        SpawnObjects();

    }

    public void UpdateBounds()
    {


    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        bool isColliding;

        do
        {
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomY = 0.5f;
            float randomZ = Random.Range(minBounds.z, maxBounds.z);
            randomPosition = new Vector3(randomX, randomY, randomZ);

            RaycastHit hit;
            isColliding = meshCollider.Raycast(new Ray(randomPosition + Vector3.up * 10f, Vector3.down), out hit, 20f);
        }
        while (isColliding);

        return randomPosition;
    }
}
