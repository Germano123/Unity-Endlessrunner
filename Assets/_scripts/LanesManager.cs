using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesManager : Singleton<LanesManager>
{
    
    // control variable of how wide is lane
    [SerializeField] float laneOffset = 2f;

    public Vector3 GetLanePosition(int laneIndex)
    {
        return laneIndex * laneOffset * Vector3.right;
    }

    void OnDrawGizmos()
    {
        // setup gizmos color
        Gizmos.color = Color.red;
        // get first lane position
        Vector3 lanepos = GetLanePosition(-1);
        // draw wire sphere
        Gizmos.DrawWireSphere(lanepos, .5f);
        // get second lane position in first position
        lanepos = GetLanePosition(0);
        // draw wire sphere in the second position
        Gizmos.DrawWireSphere(lanepos, .5f);
        // get thid lane position
        lanepos = GetLanePosition(1);
        // draw wire sphere in third position
        Gizmos.DrawWireSphere(lanepos, .5f);
    }
}
