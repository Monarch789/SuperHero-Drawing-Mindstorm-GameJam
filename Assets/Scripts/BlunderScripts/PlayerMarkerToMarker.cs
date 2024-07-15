using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarkerToMarker : MonoBehaviour
{
    public float speed = 10f; // Speed of the player movement
    private Transform[] markers; // Array to store marker positions
    public GameObject slashPrefab; // Prefab for the slash effect
    private List<Transform> remainingMarkers = new List<Transform>(); // List to track unvisited markers
    private List<Transform> visitedMarkers = new List<Transform>(); // List to track visited markers
    private bool isFirstMove = true; // Flag to check if it's the first move

    void Start()
    {
        // Find all markers in the scene
        GameObject[] markerObjects = GameObject.FindGameObjectsWithTag("Marker");
        markers = new Transform[markerObjects.Length];

        for (int i = 0; i < markerObjects.Length; i++)
        {
            markers[i] = markerObjects[i].transform;
            remainingMarkers.Add(markers[i]); // Add all markers to the list of remaining markers
        }

        // Start the movement coroutine
        StartCoroutine(MoveToRandomMarker());
    }

    IEnumerator MoveToRandomMarker()
    {
        while (remainingMarkers.Count > 1)
        {
            // Choose a random index from remaining markers
            int randomIndex = Random.Range(0, remainingMarkers.Count);
            Transform nextMarker = remainingMarkers[randomIndex];

            Vector2 startPosition = transform.position;
            Vector2 endPosition = nextMarker.position;

            Debug.Log($"Moving to marker at position {endPosition}");

            // Move towards the chosen marker
            while (Vector2.Distance(transform.position, endPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
                yield return null;
            }

            // Skip drawing slash for the first move
            if (!isFirstMove)
            {
                // Draw slash from startPosition to endPosition
                DrawSlash(startPosition, endPosition);
            }
            else
            {
                isFirstMove = false;
            }

            // Remove visited marker from remaining markers and add to visited markers
            remainingMarkers.RemoveAt(randomIndex);
            visitedMarkers.Add(nextMarker);

            // Wait for a short duration before moving to the next marker
            yield return new WaitForSeconds(0.1f); // Reduced wait time for quick slashes
        }

        Debug.Log("All markers visited, deleting all slashes and visited markers");
        DeleteAllSlashesAndVisitedMarkers();
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

        // Destroy the slash effect after a short duration
        // Destroy(slash, 0.5f);
    }

    void DeleteAllSlashesAndVisitedMarkers()
    {
        // Find all current slashes in the scene
        GameObject[] existingSlashes = GameObject.FindGameObjectsWithTag("Slash");

        // Destroy each slash game object
        foreach (var slash in existingSlashes)
        {
            Destroy(slash, 0.2f);
        }

        // Destroy all visited markers
        foreach (var marker in visitedMarkers)
        {
            Destroy(marker.gameObject);
        }
    }
}
