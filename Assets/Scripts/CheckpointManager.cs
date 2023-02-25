using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager cm;

    public Vector3 recentCheckpoint = Vector3.zero;

    private void Awake()
    {
        if(cm == null)
        {
            cm = GetComponent<CheckpointManager>();
        }
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        recentCheckpoint = newCheckpoint;
    }

    public Vector3 GetCheckpoint()
    {
        return recentCheckpoint;
    }

}
