using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    public GameObject[] numbersPrefabs;
    public GameObject questionsContainer;
    public GameObject optionsContainer;

    public void Start()
    {
        InstantiateNumbers();
    }

    private void InstantiateNumbers()
    {
        int firstNumberValue = Random.Range(2, 10);
        GameObject firstNumberObj = Instantiate(numbersPrefabs[firstNumberValue], questionsContainer.transform);
        firstNumberObj.transform.localPosition = new Vector3(0.4f, 0f, 0f);
        firstNumberObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        int secondNumberValue = Random.Range(2, 10);

        int productValue = firstNumberValue * secondNumberValue;
        if (productValue >= 10)
        {
            int tensPlaceValue = productValue / 10;
            GameObject tensPlaceObj = Instantiate(numbersPrefabs[tensPlaceValue], questionsContainer.transform);
            tensPlaceObj.transform.localPosition = new Vector3(-0.3f, 0f, 0f);
            tensPlaceObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            int onesPlaceValue = productValue % 10;
            GameObject onesPlaceObj = Instantiate(numbersPrefabs[onesPlaceValue], questionsContainer.transform);
            onesPlaceObj.transform.localPosition = new Vector3(-0.4f, 0f, 0f);
            onesPlaceObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            GameObject productObj = Instantiate(numbersPrefabs[productValue], questionsContainer.transform);
            productObj.transform.localPosition = new Vector3(-0.3f, 0f, 0f);
            productObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        List<int> optionValues = new List<int>();
        optionValues.Add(secondNumberValue);

        while (optionValues.Count < 4)
        {
            int optionValue = Random.Range(2, 10);
            if (optionValue != secondNumberValue && !optionValues.Contains(optionValue))
                optionValues.Add(optionValue);
        }

        Shuffle(optionValues); 

        float optionSpacing = 0.3f;
        float optionOffset = (optionValues.Count - 1) * optionSpacing;

        for (int i = 0; i < optionValues.Count; i++)
        {
            int optionValue = optionValues[i];
            GameObject optionObj = Instantiate(numbersPrefabs[optionValue], optionsContainer.transform);
            optionObj.name = "wrong";
            if (optionValue == secondNumberValue)
            {
                optionObj.name = "correct";
            }

            optionObj.transform.localPosition = new Vector3(-optionSpacing * i - optionOffset, 0f, 0f);
            optionObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
