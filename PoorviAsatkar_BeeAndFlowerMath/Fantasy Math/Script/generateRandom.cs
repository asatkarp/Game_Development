using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateRandom : MonoBehaviour
{
    int av;
    int bv;
    int answer = 0;
    [SerializeField]
    private GameObject[] _Numbers;
    [SerializeField]
    private GameObject Prefab, grid_layout_opt_prefab, grid_layout_q;
    private List<int> optionList = new List<int>();
    public GameObject grid_layout_opt;

    private void Start()
    {
        grid_layout_opt = Instantiate(grid_layout_opt_prefab, new Vector3(6f, 4f, 3f), Quaternion.Euler(0, 180, 0));
        av = Random.Range(1, 10);
        SetNumber(av, 0);
        bv = Random.Range(2, 11);
        SetNumber(bv, 0);
        answer = av * bv;
        int num1 = Random.Range(4, 100);
        int num2 = Random.Range(6, 100);
        while (num1 == answer) { num1 = Random.Range(3, 100); }
        while (num2 == answer || num2 == num1) { num2 = Random.Range(2, 100); }
        optionList.Add(num1);
        optionList.Add(num2);
        optionList.Add(answer);
        InstantiateRandomOption();

        Invoke("DeleteElements", 4f);
        Invoke("Start", 6f);
    }

    public void SetNumber(int Temp, int opt)
    {
        GameObject numberHolder = Instantiate(Prefab, Vector3.zero, Quaternion.Euler(0, 180, 0));

        if (Temp < 10)
        {
            GameObject a = Instantiate(_Numbers[Temp], new Vector3(0, 0, -0.3f), Quaternion.Euler(0, 180, 0));
            a.transform.SetParent(numberHolder.transform);
        }
        else
        {
            GameObject a = Instantiate(_Numbers[Temp / 10], new Vector3(-0.35f, 0, -0.3f), Quaternion.Euler(0, 180, 0));
            GameObject b = Instantiate(_Numbers[Temp % 10], new Vector3(0.35f, 0, -0.3f), Quaternion.Euler(0, 180, 0));
            a.transform.SetParent(numberHolder.transform);
            b.transform.SetParent(numberHolder.transform);
        }

        if (opt == 1)
        {
            numberHolder.transform.SetParent(grid_layout_opt.transform);
            if (Temp == answer)
            {
                numberHolder.name = "correct";
            }
            else
            {
                numberHolder.name = "wrong";
            }
        }
        else
        {
            numberHolder.transform.SetParent(grid_layout_q.transform);
        }
    }

    private void DeleteElements()
    {
        GameObject[] optionObjects = GameObject.FindGameObjectsWithTag("option");
        foreach (GameObject optionObject in optionObjects)
        {
            Destroy(optionObject);
        }
    }

    private void InstantiateRandomOption()
    {
        while (optionList.Count > 0)
        {
            int randomIndex = Random.Range(0, optionList.Count);
            int option = optionList[randomIndex];
            SetNumber(option, 1);
            optionList.RemoveAt(randomIndex);
        }
    }
}
