using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform transformA;
    [SerializeField] private Transform transformB;

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;


    // Start is called before the first frame update
    void Start()
    {
        posA = transformA.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transformA.localPosition = Vector3.MoveTowards(transformA.localPosition, nexPos, speed * Time.deltaTime);
        if (Vector3.Distance(transformA.localPosition, nexPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nexPos = nexPos != posA ? posA : posB;
    }
}
