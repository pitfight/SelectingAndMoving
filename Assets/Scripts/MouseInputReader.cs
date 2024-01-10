using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class MouseInputReader : MonoBehaviour
{
    private const float MAX_NAV_MESH_DISTANCE = 1f;
    private const float MAX_RAYCAST_DISTANCE = 200f;

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, MAX_RAYCAST_DISTANCE, mouseColliderLayerMask))
            {
                if (NavMesh.SamplePosition(raycastHit.point, out var navMeshHit, MAX_NAV_MESH_DISTANCE, NavMesh.AllAreas))
                {
                    transform.position = navMeshHit.position;
                }
            }
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
