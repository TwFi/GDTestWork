using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    Transform camT;

    private void Awake()
    {
        camT = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camT.rotation * Vector3.forward, camT.rotation * Vector3.up);
    }
}
