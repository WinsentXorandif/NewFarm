using UnityEngine;

public class TimerView : MonoBehaviour
{
    private Transform mainCamTransform;

    void Start()
    {
        mainCamTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamTransform.forward);
    }
}
