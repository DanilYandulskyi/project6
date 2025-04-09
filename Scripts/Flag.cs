using UnityEngine;
using System;

public class Flag : MonoBehaviour
{
    public event Action Disabled;
    
    public void Disable()
    {
        Disabled?.Invoke();
        gameObject.SetActive(false);
    }
}
