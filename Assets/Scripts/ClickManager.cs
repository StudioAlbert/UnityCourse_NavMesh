using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{

    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private float _detectionDepth = 1000;
    [SerializeField] private GameObject _clickPoint;

    Interactable _hoverObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

            Hover(hit);

            if (mouse.leftButton.wasPressedThisFrame)
            {
                Click(hit);
            }

        }
    }
    private void Click(RaycastHit hit)
    {

        if (hit.collider.TryGetComponent(out Interactable interactable))
        {
            interactable.SwithchActivate();
        }else if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, 1, NavMesh.AllAreas))
        {
            FollowDirection[] bots = FindObjectsByType<FollowDirection>(FindObjectsSortMode.None);
            foreach (var bot in bots)
            {
                bot.SetDirection(hit.point);
            }

            _clickPoint.transform.position = hit.point;

        }
    }
    private void Hover(RaycastHit hit)
    {

        if (_hoverObject && hit.collider.gameObject != _hoverObject.gameObject)
        {
            _hoverObject.Hover(false);
        }

        if (hit.collider.TryGetComponent(out Interactable interactable))
        {
            Debug.Log("Hovered : " + interactable.gameObject.name);
            _hoverObject = interactable;
            _hoverObject.Hover(true);
        }

    }
}
