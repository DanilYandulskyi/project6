using UnityEngine;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _scanRadius;

    public List<Gold> Scan()
    {
        List<Gold> goldList = new List<Gold>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Gold gold))
            {
                goldList.Add(gold);
            }
        }

        return goldList;
    }
}