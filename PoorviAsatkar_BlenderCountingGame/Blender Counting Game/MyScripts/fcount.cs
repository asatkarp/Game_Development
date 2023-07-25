using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fcount : MonoBehaviour
{
    [SerializeField]
    public Transform Layout1;
    public Text stat;
    public Text countGrapeText;
    private int countGrape;

    private void Start()
    {
        GeneratecountGrape();
        countGrapeText.text = countGrape.ToString();
    }

    public void countFruits()
    {
        int count = Layout1.childCount;
        Debug.Log(count);
        if (count == countGrape)
        {
            stat.text = "correct!";
            StartCoroutine(RestartScene());
        }
        else
        {
            stat.text = "Try Again!";
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GeneratecountGrape()
    {
        countGrape = Random.Range(1, 5);
    }
}
