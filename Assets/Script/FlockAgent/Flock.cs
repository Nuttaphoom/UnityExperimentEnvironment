using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehavior behavior;

    List<FlockAgent> agents = new List<FlockAgent>();

    [Range(10, 500)]
    public int startingCount;

}
