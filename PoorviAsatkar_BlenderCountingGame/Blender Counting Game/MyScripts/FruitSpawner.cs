using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public GameObject fruit;
    public GameObject newFruit;
    public GameObject curveLayout;
    public void CreateFruit()
    {
        Vector3 newpos = fruit.transform.position;
        // newpos.z = 9.3f;
        newFruit = (GameObject)Instantiate(fruitPrefab, newpos, Quaternion.identity);
        newFruit.layer = fruit.layer;
        // newFruit.transform.SetParent(curveLayout.transform, true);
        newFruit.transform.SetParent(fruit.transform.parent);

    }
    public void destroyFruit()
    {
        Destroy(newFruit);
    }

}
