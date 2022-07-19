using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _points;
    
    [SerializeField]
    private float _minDistance;

    public void SetTotal(float distance)
    {
        var total = Mathf.FloorToInt(distance / _minDistance);
        total = Mathf.Clamp(total, 0, _points.Count);
        
        ActivePoints(total);
    }

    private void ActivePoints(int total)
    {
        foreach (var point in _points)
        {
            point.SetActive(total > 0);

            total--;
        }
    }
}
