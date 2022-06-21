using System.Collections.Generic;
using UnityEngine;

public class WaterChunkManager : MonoBehaviour {

    public GameObject waterPrototype;
    public float waterLevel;

    static float meshSize;

    private readonly Queue<GameObject> inactiveWaterChunkPool = new();


    private void Awake() {
        meshSize = GetComponentInParent<TerrainGenerator>().meshSettings.MeshWorldSize;
    }


    void Start() {
        // Transform prototype to unit size to avoid incorrect scaling
        waterPrototype.transform.localScale = Vector3.one;
        // Get actual game size of unit size prototype
        Vector3 waterPrototypeSize = waterPrototype.GetComponent<Renderer>().bounds.size;
        // Scale prototype to chunk size
        waterPrototype.transform.localScale = new(meshSize / waterPrototypeSize.x, 1, meshSize / waterPrototypeSize.z);

        // Store prototype to Pool
        DisposeWaterChunk(waterPrototype);
    }


    public void DisposeWaterChunk(GameObject waterToDispose) {
        waterToDispose.SetActive(false);
        inactiveWaterChunkPool.Enqueue(waterToDispose);
    }


    public GameObject GetWaterChunk(Vector2 waterPosition) {
        GameObject waterToReturn;
        Vector3 worldWaterPosition = new(waterPosition.x, waterLevel, waterPosition.y);

        // If there are pooled inactive water chunks, return one of them
        if (inactiveWaterChunkPool.Count > 0) {
            waterToReturn = inactiveWaterChunkPool.Dequeue();
            waterToReturn.transform.position = worldWaterPosition;
        } else {
            // If there are no pooled inactive water chunks, instantiate new chunk from prototype
            waterToReturn = Instantiate(waterPrototype, worldWaterPosition, Quaternion.identity, transform);
        }

        waterToReturn.SetActive(true);
        return waterToReturn;
    }
}
