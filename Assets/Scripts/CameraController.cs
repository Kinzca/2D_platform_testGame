using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform rightPoint;
    public Transform target;
    public Transform farBackGround, middleBackGround;
    public float minHeight, maxHeight;

    public float stopPoint;

    public bool stopFollow;

    private void Awake()
    {
        instance = this;
    }

    //private float  lastXPos;
    private Vector2 lastXPos;
    // Start is called before the first frame update

    void Start()
    {
        lastXPos = transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
        {
            //transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

            transform.position = new Vector3(Mathf.Clamp(target.position.x, rightPoint.transform.position.x, Mathf.Infinity), Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            //float amountToMoveX = transform.position.x - lastXPos;
            Vector2 amountToMove = new Vector3(transform.position.x - lastXPos.x, transform.position.y - lastXPos.y);

            farBackGround.position += new Vector3(amountToMove.x, 0f, 0f);
            middleBackGround.position += new Vector3(amountToMove.x * 0.5f, amountToMove.y * 0.5f, 0f) * 0.5f;

            lastXPos = transform.position;
        }

        

    }

}
