using UnityEngine;
using UnityEditor;

/// <summary>
/// Optional lightweight custom editor that allows for calling DistributeSounds from the Inspector.
/// </summary>
[CustomEditor(typeof(DistributeObjects))]
public class DistributeAudioObjectsEditor : Editor
{
    DistributeObjects script;

    public override void OnInspectorGUI()
    {
        // Allows this to call the target functions
        if (!script)
        {
            script = target as DistributeObjects;
        }

        // Display Buttons for functions
        if (GUILayout.Button("Distribute"))
        {
            script.DistributeSounds();
        }

        if (GUILayout.Button("Clear"))
        {
            script.ClearList();
        }

        base.OnInspectorGUI();
    }
}