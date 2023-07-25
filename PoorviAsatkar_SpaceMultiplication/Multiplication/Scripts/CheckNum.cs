using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckNumber : MonoBehaviour
{
    [SerializeField]
    public GameObject tick, wrong;
    public GameObject layoutObject;
    public int isCorrect = 0;
    public Color lightGreenColor;

    private int currentLightIndex = 0;

    public void CheckNumberInLayout()
    {
        Transform layoutTransform = layoutObject.transform;
        GameObject numberInLayout = FindNumberInLayout(layoutTransform);

        if (numberInLayout != null)
        {
            Debug.Log(numberInLayout.tag);
            Debug.Log(numberInLayout.name);
            if (numberInLayout.name == "correct")
            {
                isCorrect = 1;
                DestroyAllNumberPrefabs();
                StartCoroutine(Status(tick));
                SetNextLightColor();
            }
            else if (numberInLayout.name == "wrong")
            {
                StartCoroutine(Status(wrong));
                Destroy(numberInLayout);
            }
        }
    }

    private GameObject FindNumberInLayout(Transform parent)
    {
        if (parent.childCount > 0)
        {
            return parent.GetChild(0).gameObject;
        }

        return null;
    }

    private void SetNextLightColor()
    {
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("light");

        if (currentLightIndex < lightObjects.Length)
        {
            Image lightImage = lightObjects[currentLightIndex].GetComponent<Image>();
            if (lightImage != null)
            {
                lightImage.color = lightGreenColor;
            }
            currentLightIndex++;
        }
    }

    private IEnumerator Status(GameObject objectPrefab)
    {
        GameObject instantiatedObject = Instantiate(objectPrefab, new Vector3(0f, 1.5f, 10f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Destroy(instantiatedObject);
    }

    private void DestroyAllNumberPrefabs()
    {
        GameObject[] numberObjects = GameObject.FindGameObjectsWithTag("numberPrefabs");

        foreach (GameObject numberObject in numberObjects)
        {
            Destroy(numberObject);
        }
    }
}
