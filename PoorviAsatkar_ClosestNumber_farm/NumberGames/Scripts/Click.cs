using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    private Numbers numbersScript;
    // [SerializeField]
    // public GameObject clickedObject;
    private void Start()
    {
        numbersScript = FindObjectOfType<Numbers>();
    }

    public void OnClick()
    {
        numbersScript.OnNumberClicked(gameObject);
        // numbersScript.OnNumberClicked(clickedObject);
    }
}
