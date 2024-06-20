using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MainPoint current;
    public float moveSpeed = 10f;
    public bool levelLoading;
    public LSManager theManager;

    public static LSPlayer instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,current.transform.position,moveSpeed*Time.deltaTime);

        if (Vector3.Distance(transform.position,current.transform.position)<.1f&&! levelLoading)
        {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (current.Right != null)
                {
                    SetNextPoint(current.Right);
                }
            }
            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (current.Left != null)
                {
                    SetNextPoint(current.Left);
                }
            }
            if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (current.up != null)
                {
                    SetNextPoint(current.up);
                }
            }
            if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (current.down != null)
                {
                    SetNextPoint(current.down);
                }
            }

            if (current.isLevel && current.levelToLoad != "" && !current.isLocked)
            {
                LSUIController.instance.ShowInfo(current);
                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    theManager.LoadLevel();

                    SoundManager.instance.PlaySFX(4);
                }
            }
        }
    }

    public void SetNextPoint(MainPoint nextPoint)
    {
        current = nextPoint;

        LSUIController.instance.HideInfo();

        SoundManager.instance.PlaySFX(5);
    }
}
