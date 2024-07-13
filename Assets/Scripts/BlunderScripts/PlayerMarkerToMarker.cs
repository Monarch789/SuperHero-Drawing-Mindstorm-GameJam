using System.Collections;
using UnityEngine;

public class PlayerMarkerToMarker : MonoBehaviour
{
    public float speed = 10f; // Speed of the player movement
    private Transform[] markers; // Array to store marker positions
    public GameObject slashPrefab; // Prefab for the slash effect
    private int currentMarkerIndex = 0;

    void Start()
    {
        // Find all markers in the scene
        GameObject[] markerObjects = GameObject.FindGameObjectsWithTag("Marker");
        markers = new Transform[markerObjects.Length];

        for (int i = 0; i < markerObjects.Length; i++)
        {
            markers[i] = markerObjects[i].transform;
        }

        // Start the movement coroutine
        StartCoroutine(MoveToRandomMarker());
    }

    // This method should be called when the finger is released
    //public void OnFingerRelease()
    //{
    //    StartCoroutine(MoveToRandomMarkers());
    //}

    IEnumerator MoveToRandomMarker()
    {
        while (true)
        {
            for (int i = 0; i < markers.Length; i++)
            {
                // Choose a random marker index
                currentMarkerIndex = Random.Range(0, markers.Length);
                Vector2 startPosition = transform.position;
                Vector2 endPosition = markers[currentMarkerIndex].position;

                Debug.Log($"Moving to marker {currentMarkerIndex} at position {endPosition}");

                // Move towards the chosen marker
                while (Vector2.Distance(transform.position, endPosition) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
                    yield return null;
                }

                Debug.Log($"Reached marker {currentMarkerIndex}, drawing slash");

                // Draw slash from startPosition to endPosition
                DrawSlash(startPosition, endPosition);

                // Wait for a short duration before moving to the next marker
                yield return new WaitForSeconds(0.1f); // Reduced wait time for quick slashes
            }

            Debug.Log("Finished moving to all markers");
        }
    }

    void DrawSlash(Vector2 start, Vector2 end)
    {
        GameObject slash = Instantiate(slashPrefab, start, Quaternion.identity);
        Vector2 direction = end - start;
        float distance = direction.magnitude;

        // Set the slash position and rotation
        slash.transform.position = (start + end) / 2;
        slash.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        // Adjust the size of the sprite to match the distance
        slash.transform.localScale = new Vector3(distance, slash.transform.localScale.y, slash.transform.localScale.z);

        Debug.Log($"Drawing slash from {start} to {end} with distance {distance}");

        Destroy(slash, 0.5f); // Destroy the slash effect after a short duration
    }
}
