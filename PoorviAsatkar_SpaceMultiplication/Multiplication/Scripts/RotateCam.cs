using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Camera rotatingCamera;
    public GameObject equationprefab;
    public GameObject checkNumberObject;
    CheckNumber checkNumberScript;

    public float rotationSpeed;
    public float rotationDelay;
    private float targetRotation = 0f;
    private bool isRotating = false;
    private int correctCount = 0;

    private void Start()
    {
        checkNumberScript = checkNumberObject.GetComponent<CheckNumber>();
        rotationDelay = 0f;
        rotationSpeed = 100f;
    }

    private void Update()
    {
        int isCorrectValue = checkNumberScript.isCorrect;
        Debug.Log(isCorrectValue);

        if (isCorrectValue == 1)
        {
            correctCount++;
            checkNumberScript.isCorrect = 0;

            StartCoroutine(RotatePrefab());
        }

        if (correctCount >= 3 && !isRotating)
        {
            isRotating = true;
            Rigidbody prefabRigidbody = equationprefab.GetComponent<Rigidbody>();
            if (prefabRigidbody != null)
            {
                prefabRigidbody.isKinematic = false;
            }
        }

        if (isRotating)
        {
            targetRotation += rotationSpeed * Time.deltaTime;
            rotatingCamera.transform.eulerAngles = new Vector3(rotatingCamera.transform.eulerAngles.x, targetRotation, rotatingCamera.transform.eulerAngles.z);

            if (targetRotation >= 180f)
            {
                targetRotation -= 360f;
            }
        }
    }

    private IEnumerator RotatePrefab()
    {
        float startRotation = equationprefab.transform.localRotation.eulerAngles.x;
        float endRotation = startRotation + 360f;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * (rotationSpeed / 180f);
            float newRotation = Mathf.Lerp(startRotation, endRotation, t);
            if (equationprefab != null)
            {
                equationprefab.transform.localRotation = Quaternion.Euler(newRotation, 0f, 90f);

            }
            yield return null;
        }
        if (equationprefab != null)
        {
            Questions questionsScript = equationprefab.GetComponent<Questions>();
            if (questionsScript != null)
            {
                questionsScript.Start();
            }
        }
    }
}
