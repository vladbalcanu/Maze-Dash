using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private IInteractable _interactable;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numberFound;


    private void Update()
    {
        _numberFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        if (_numberFound != 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            if (_interactable != null)
            {
                Debug.Log("Interactable not null");
                if (!_interactionPromptUI.isDisplayed)
                    _interactionPromptUI.SetUp(_interactable.interactionPrompt);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                    _interactable.Interact(this);
            }

        }
        else
        {
            if (_interactable != null)
                _interactable = null;

            if (_interactionPromptUI.isDisplayed)
                _interactionPromptUI.Close();
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
