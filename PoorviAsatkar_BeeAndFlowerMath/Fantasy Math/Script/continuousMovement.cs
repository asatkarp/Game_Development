using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{

    private float speed = 5f;
    public GameObject player;
    private bool isMoving = true;
    public generateRandom generator;
    public ParticleSystem red, redcircle, green, greencircle;

    private float[] positions = { 5.5f, 2.5f, -0.5f };
    private int currentPositionIndex = 1;
    [SerializeField]
    private Animator flower;

    private void Update()
    {

        if (isMoving && generator.grid_layout_opt != null)
        {
            generator.grid_layout_opt.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToNextPosition(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToNextPosition(-1);
        }
    }

    void OnTriggerEnter(Collider collided)
    {
        Debug.Log(collided.name);
        if (collided.name == "correct")
        {
            if (!flower.GetBool("bloom")) // Check if bloom parameter is false
            {
                flower.SetBool("bloom", true);
                flower.SetBool("wilt", false);// Set bloom parameter to true
            }
            green.Play();
            greencircle.Play();

        }
        else if (collided.name == "wrong")
        {
            if (!flower.GetBool("wilt")) // Check if bloom parameter is false
            {
                flower.SetBool("wilt", true);
                flower.SetBool("bloom", false); // Set bloom parameter to true
            }
            red.Play();
            redcircle.Play();
        }
        DestroyChildren(collided.gameObject);
        // isMoving = true;
    }
    private void DestroyChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void MoveToNextPosition(int direction)
    {
        currentPositionIndex += direction;

        if (currentPositionIndex < 0)
        {
            currentPositionIndex = positions.Length - 1;
        }
        else if (currentPositionIndex >= positions.Length)
        {
            currentPositionIndex = 0;
        }

        Vector3 newPosition = transform.position;
        newPosition.y = positions[currentPositionIndex];
        transform.position = newPosition;
    }
}
