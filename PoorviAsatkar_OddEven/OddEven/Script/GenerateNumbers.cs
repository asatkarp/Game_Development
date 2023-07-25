using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNumbers : MonoBehaviour
{
    [SerializeField]
    private Text odd, even, over;
    [SerializeField]
    private GameObject[] _Numbers;
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private GameObject gridLayout;
    [SerializeField]
    private GameObject evenGrid;
    [SerializeField]
    private GameObject oddGrid;
    private List<int> numberList = new List<int>();

    private void Start()
    {
        GenerateRandomNumbers();
        InstantiateNumbers();
        // CheckChildrenNames();
    }

    private void GenerateRandomNumbers()
    {
        HashSet<int> oddNumbers = new HashSet<int>();
        HashSet<int> evenNumbers = new HashSet<int>();

        while (oddNumbers.Count < 5)
        {
            int oddNumber = Random.Range(1, 10) * 2 - 1;
            if (!oddNumbers.Contains(oddNumber))
            {
                oddNumbers.Add(oddNumber);
                numberList.Add(oddNumber);
            }
        }

        while (evenNumbers.Count < 5)
        {
            int evenNumber = Random.Range(1, 10) * 2;
            if (!evenNumbers.Contains(evenNumber))
            {
                evenNumbers.Add(evenNumber);
                numberList.Add(evenNumber);
            }
        }
    }
    private void InstantiateNumbers()
    {
        ShuffleNumberList();

        foreach (int number in numberList)
        {
            SetNumber(number);
        }
    }

    private void ShuffleNumberList()
    {
        System.Random rng = new System.Random();
        int n = numberList.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = numberList[k];
            numberList[k] = numberList[n];
            numberList[n] = temp;
        }
    }

    private void SetNumber(int number)
    {
        GameObject numberHolder = Instantiate(Prefab, position: Vector3.zero, Quaternion.identity);

        if (number < 10)
        {
            GameObject num = Instantiate(_Numbers[number], Vector3.zero, Quaternion.identity);
            num.transform.SetParent(numberHolder.transform, false);
        }
        else
        {
            GameObject tens = Instantiate(_Numbers[number / 10], new Vector3(0.3f, 0, 0f), Quaternion.identity);
            GameObject ones = Instantiate(_Numbers[number % 10], new Vector3(-0.3f, 0, 0f), Quaternion.identity);
            tens.transform.SetParent(numberHolder.transform, false);
            ones.transform.SetParent(numberHolder.transform, false);

        }
        string parentName = (number % 2 == 0) ? "even" : "odd";
        numberHolder.transform.SetParent(gridLayout.transform, false);
        numberHolder.name = parentName;
    }

    public void CheckChildrenNames()
    {
        System.String e = "";
        System.String o = "";
        if (evenGrid.transform.childCount != 5)
        {
            e = "Drag all even cards";
        }
        else
        {
            foreach (Transform child in evenGrid.transform)
            {
                if (child.name != "even")
                {
                    e = "Wrong";
                    break;
                }
                else
                {
                    e = "Correct";
                }
            }
        }
        even.text = e;

        if (oddGrid.transform.childCount != 5)
        {
            o = "Drag all odd cards";
        }
        else
        {
            foreach (Transform child in oddGrid.transform)
            {
                if (child.name != "odd")
                {
                    o = "Wrong";
                    break;
                }
                else
                {
                    o = "Correct";
                }
            }
        }
        odd.text = o;
        if (e == "Correct" && o == "Correct")
        {
            over.text = "Good Job!!";
        }
    }

}
