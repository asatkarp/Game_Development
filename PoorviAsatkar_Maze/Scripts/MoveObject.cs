using System.Collections;
using System.Collections.Generic;
using log4net.Repository.Hierarchy;
// using System.Numerics;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private CharacterController _controller;

    private Vector3 _movement;
    private GameObject player;

    [SerializeField]
    private GameObject fencePrefab;
    [SerializeField]
    private GameObject flagPrefab;
    private Vector3 startPos = new Vector3(429, 21, -466);

    private Vector3[] fenceCoords = new Vector3[] {
    new Vector3(-2,25,498),
    new Vector3(-508,25,-4),
    new Vector3(-2,25,-510),
    new Vector3(496,25,-4)

};

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _movement = Vector3.zero;
        _movement.y -= 100f * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _movement.x += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _movement.z += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _movement.z -= 1;
        }
        _controller.Move(_movement);
        // if (player.transform.position.x < -403 && player.transform.position.z > 360)
        // {
        //     Vector3 flagPos = new Vector3(-359.000366f, 9.60000038f, 475.160004f);
        //     Quaternion flagRotation = Quaternion.Euler(90f, 90f, 0f);
        //     GameObject flag = Instantiate(flagPrefab, flagPos, flagRotation);
        //     flag.transform.localScale = new Vector3(4.5000067f, 7f, 5.9000072f);
        //     flag.tag = "Flag";
        // }
        if (player.transform.position.y < -200f)
        {
            // Respawn player
            player.transform.position = startPos;
            GameObject planeObject = GameObject.FindWithTag("Plane");

            Quaternion rotation;
            for (int i = 0; i < 4; i++)
            {
                Vector3 fencePos = fenceCoords[i];
                Debug.Log("Spawning fence at position: " + fencePos);
                if (i % 2 == 0)
                {
                    rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else
                {
                    rotation = Quaternion.identity;
                }
                GameObject newFence = Instantiate(fencePrefab, fencePos, rotation);
                newFence.tag = "Fence";
                newFence.transform.SetParent(planeObject.transform);
                newFence.transform.localScale = new Vector3(0.2f, 5f, 10f);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("hit");
        if (hit.gameObject.tag == "Fence")
        {
            Debug.Log("hit fence");
            Debug.Log(player.transform.position);
            GameObject[] arrayFence = GameObject.FindGameObjectsWithTag("Fence");
            foreach (GameObject obj in arrayFence)
            {
                Destroy(obj);
            }
        }
        if (hit.gameObject.tag == "Finish")
        {
            Vector3 flagPos = new Vector3(-359.000366f, 9.60000038f, 475.160004f);
            Quaternion flagRotation = Quaternion.Euler(90f, 90f, 0f);
            GameObject flag = Instantiate(flagPrefab, flagPos, flagRotation);
            flag.transform.localScale = new Vector3(4.5000067f, 7f, 5.9000072f);
            flag.tag = "Flag";
        }

    }
}