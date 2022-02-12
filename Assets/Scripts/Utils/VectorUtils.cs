using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class VectorUtils
{
    public static Vector3 GetCenterPoint(List<Vector3> points)
    {
        var bound = new Bounds(points[0], Vector3.zero);
        for(int i = 1; i < points.Count(); i++)
        {
            bound.Encapsulate(points[i]);
        }

        return bound.center;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}