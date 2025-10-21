using System;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _hoverMaterial;
    [SerializeField] private Material _activatedMaterial;
    [SerializeField] private List<Behaviour> _lockableComponents;

    private bool _hovered, _activated;
    private Material _startMaterial;

    public bool Activated => _activated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_renderer)
        {
            _startMaterial = _renderer.material;
        }
    }

    private void Update()
    {
        
        foreach (var component in _lockableComponents)
        {
            component.enabled = _activated;
        }
        
        if (_activated)
        {
            SwitchMaterial(_activatedMaterial);
        }else if (_hovered)
        {
            SwitchMaterial(_hoverMaterial);
        }
        else
        {
            SwitchMaterial(_startMaterial);
        }
    }
    public void SwithchActivate()
    {
        _activated = !_activated;
    }
    public void Hover(bool hovered)
    {
        _hovered = hovered;
    }
        
    private void SwitchMaterial(Material mat)
    {
        if (_renderer)
        {
            if (_renderer.material != mat)
            {
                _renderer.material = mat;
            }
        }
    }



}
