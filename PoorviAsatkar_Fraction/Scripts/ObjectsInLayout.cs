using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    [SerializeField]
    public Transform circleLayout;
    [SerializeField]
    public Transform gridLayout;
    [SerializeField]
    public Text circleCountText;
    [SerializeField]
    public Text gridCountText;
    [SerializeField]
    public Text stat;
    public void CountObjects()
    {
        int score = 0;
        int circleCount = circleLayout.childCount;
        int gridCount = gridLayout.childCount;
        int gridSphereCount = 0;
        int circleSphereCount = 0;

        for (int i = 0; i < gridCount; i++)
        {
            Transform child = gridLayout.GetChild(i);
            if (child.GetComponent<MeshFilter>() != null && child.GetComponent<MeshFilter>().mesh.name.Contains(value: "Sphere"))
            {
                gridSphereCount++;
            }
        }

        for (int i = 0; i < circleCount; i++)
        {
            Transform child = circleLayout.GetChild(i);
            if (child.GetComponent<MeshFilter>() != null && child.GetComponent<MeshFilter>().mesh.name.Contains("Sphere"))
            {
                circleSphereCount++;
            }
        }


        if (gridCount == 8 && gridSphereCount == 2)
        {
            score += 1;
            gridCountText.text = "Fraction of Spheres in Grid Layout: " + gridSphereCount.ToString() + "/" + gridCount.ToString();

        }
        else
        {
            gridCountText.text = gridSphereCount.ToString() + "/" + gridCount.ToString() + " Try Again!";
        }

        if (circleCount == 7 && circleSphereCount == 3)
        {
            score += 1;
            circleCountText.text = "Fraction of Spheres in Circle Layout: " + circleSphereCount.ToString() + "/" + circleCount.ToString();

        }
        else
        {
            circleCountText.text = circleSphereCount.ToString() + "/" + circleCount.ToString() + " Try Again!";
        }
        if (score == 2)
        {
            stat.text = "YOU WIN!";
        }
        else
        {
            stat.text = "Try to move the objects to form the correct fraction of spheres. Good luck!";
        }
    }

}
