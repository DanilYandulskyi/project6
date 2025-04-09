using UnityEngine;

public class Gold : MonoBehaviour
{
    public void StartFollow(Transform target)
    {
        transform.parent = target;
    }

    public void StopFollow()
    {
        transform.parent = null;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}