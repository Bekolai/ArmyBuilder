using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] private float playerSpeed=10f;


    [SerializeField] AnimController animController;
    [SerializeField] Rigidbody rb;
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animController.startWalking();
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
                direction = ray.GetPoint(distance);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(direction.x, 0f, direction.z),
                playerSpeed * Time.deltaTime);

            var offset = direction - transform.position;

            if (offset.magnitude > 0.25f)
                transform.LookAt(direction);

        }
        if (Input.GetMouseButtonUp(0))
        {
            animController.stopWalking();
            rb.velocity = Vector3.zero;

        }
    }
}
