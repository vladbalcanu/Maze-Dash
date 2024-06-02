using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GameObject _characterPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    public bool isDisplayed = false;

    private void Start()
    {
        _camera = Camera.main;
        _characterPanel.SetActive(false);
    }
    private void LateUpdate()
    {
        var rotation = _camera.transform.rotation;
        transform.LookAt(transform.position+ rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _characterPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        isDisplayed = false;
        _characterPanel.SetActive(false);
    }
}

