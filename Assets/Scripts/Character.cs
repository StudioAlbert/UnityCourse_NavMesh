using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private float _detectionDepth = 1000;
    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private Transform _clickPoint;
    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;

        Ray ray = Camera.main.ScreenPointToRay(mouse.position.value);
        Debug.DrawRay(ray.origin, ray.direction * _detectionDepth, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactionLayers))
        {
            Debug.Log("Touched  : " + hit.collider.gameObject.name);

            if (mouse.leftButton.wasPressedThisFrame)
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, 1, NavMesh.AllAreas))
                {
                    if (_agent)
                    {
                        _agent.SetDestination(hit.point);
                    }
                    _clickPoint.position = hit.point;
                }
            }

        }
        
        _animator.SetFloat("velocity", _agent.velocity.magnitude);
    }
}
