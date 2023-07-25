using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class Numbers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _Numbers;
    [SerializeField]
    public GameObject egg;
    [SerializeField]
    public GameObject hatch;
    [SerializeField]
    public GameObject nest;
    public Transform parentObject1;
    public Transform parentObject2;
    private GridLayoutGroup gridLayout1;
    private GridLayoutGroup gridLayout2;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject correct;
    private Vector3 pos;
    private static int score = 0;
    private int num1, num2, num3;

    private void Start()
    {
        UpdateScore();
        pos = new Vector3(0f, 1f, 2.5f);
        nest = Instantiate(nest, pos, Quaternion.identity);
        gridLayout1 = parentObject1.GetComponent<GridLayoutGroup>();
        gridLayout2 = parentObject2.GetComponent<GridLayoutGroup>();

        num1 = Random.Range(0, 10);
        num2 = Random.Range(0, 10);
        while (num2 == num1)
        {
            num2 = Random.Range(0, 10);
        }
        num3 = Random.Range(0, 10);
        while (num3 == num2 || num3 == num1 || Mathf.Abs(num1 - num3) == Mathf.Abs(num1 - num2))
        {
            num3 = Random.Range(0, 10);
        }

        Instantiate(_Numbers[num1], parentObject1);

        obj2 = Instantiate(_Numbers[num2], parentObject2);
        obj3 = Instantiate(_Numbers[num3], parentObject2);

        findCloser(num1, num2, num3);
    }

    private void UpdateScore()
    {
        pos = new Vector3(6.32f, 0f, 3.05f);
        Quaternion rot = new Quaternion(0, -7, -7, 0);
        if (score < 10)
        {
            Instantiate(_Numbers[score], pos, rot);
            Debug.Log(score);
        }
        if (score >= 10)
        {
            pos = new Vector3(5.97f, 0f, 3.30f);
            Instantiate(_Numbers[score / 10], pos, rot);
            pos = new Vector3(6.67f, 0f, 3.30f);
            Instantiate(_Numbers[score % 10], pos, rot);
            Debug.Log(score / 10 + " " + score % 10);
        }
    }
    private void findCloser(int num1, int num2, int num3)
    {
        int distance1 = Mathf.Abs(num1 - num2);
        int distance2 = Mathf.Abs(num1 - num3);

        if (distance1 < distance2)
        {
            correct = obj2;
        }
        else
        {
            correct = obj3;
        }

    }

    public void OnNumberClicked(GameObject clickedObject)
    {
        pos = nest.transform.position;
        Destroy(nest);

        if (clickedObject == obj2 || clickedObject == obj3)
        {
            if (clickedObject == correct)
            {
                Debug.Log("Correct!");
                score += 2;
                if (clickedObject == obj2)
                {
                    Instantiate(hatch, pos, Quaternion.identity);
                    Destroy(obj3);
                }
                else if (clickedObject == obj3)
                {
                    Instantiate(hatch, pos, Quaternion.identity);
                    Destroy(obj2);
                }

            }
            else
            {
                Debug.Log("Wrong!");
                if (score > 0)
                { score -= 1; }
                if (clickedObject == obj2)
                {
                    Instantiate(egg, pos, Quaternion.identity);
                    Destroy(obj2);
                }
                else if (clickedObject == obj3)
                {
                    Instantiate(egg, pos, Quaternion.identity);
                    Destroy(obj3);
                }
            }

            StartCoroutine(RestartScene());
        }
    }


    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
