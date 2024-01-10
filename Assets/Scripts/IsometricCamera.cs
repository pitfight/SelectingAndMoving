using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    private Transform followTarget;
    [SerializeField] private float distance = 30.0f;

    void Update()
    {
        if (followTarget != null)
        {
            Vector3 dir = new Vector3(0.7f, 1f, 0.7f).normalized; // Направление для изометрического вида
            transform.position = followTarget.position - dir * -distance;
            transform.LookAt(followTarget);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        followTarget = newTarget;
    }
}
