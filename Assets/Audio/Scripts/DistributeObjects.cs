using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly distributes prefabs in a given dropArea gameobject (takes scale values of a reference gameobject, eg. with a collider).
/// </summary>
public class DistributeObjects : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject dropArea;
    public int numberOfObjectsToDistribute;
    public List<GameObject> createdObjects = new List<GameObject>();

    private Vector3 dropAreaSize;

    /// <summary>
    /// Distributes the audio object prefabs in the area defined by the Drop Area object's size.
    /// </summary>
    public void DistributeSounds()
    {
        // Size is defined by the scale values - intended so the Drop Area can use a Box Collider to visualise the area.
        dropAreaSize = dropArea.transform.localScale;

        for (int i = 0; i < numberOfObjectsToDistribute; ++i)
        {
            // Choose a random point inside the drop area.
            Vector3 randomPoint = dropArea.transform.position +
                                    new Vector3(Random.Range(-dropAreaSize.x / 2, dropAreaSize.x / 2),
                                                Random.Range(-dropAreaSize.y / 2, dropAreaSize.y / 2),
                                                Random.Range(-dropAreaSize.z / 2, dropAreaSize.z / 2));

            GameObject newObj = Instantiate<GameObject>(objectPrefab, this.transform, true);
            newObj.transform.position = randomPoint;
            newObj.name = objectPrefab.name + "-" + i;

            // Hold a reference to the created object in a List.
            createdObjects.Add(newObj);
        }
    }

    /// <summary>
    /// Destroys the created objects and resets name to what it was before DistributeSounds was called.
    /// </summary>
    public void ClearList()
    {
        foreach (GameObject obj in createdObjects)
        {
            DestroyImmediate(obj);
        }
        createdObjects = new List<GameObject>();
    }
}
