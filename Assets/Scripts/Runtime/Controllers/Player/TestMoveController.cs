using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestMoveController : MonoBehaviour
{
    public float speed;
    public int dirStat = 8;
    public Vector3 dirMove;
    public List<int> dirArray = new List<int>();
    public int lastDir;

    [SerializeField] Animator anim;


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        //This is to test if the player is pressing a direction key, if not, then idle animation.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            dirMove = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            transform.Translate(speed * dirMove.normalized * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W))
            {
                dirArray.Add(0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                dirArray.Add(3);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                dirArray.Add(1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                dirArray.Add(2);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                dirArray.Remove(0);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                dirArray.Remove(3);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                dirArray.Remove(1);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                dirArray.Remove(2);
            }

            lastDir = dirArray.Last();
            anim.SetTrigger("Run");
            anim.speed = 1;

            /*//This is the idle animation, if player has not pressed any keys.
            } else if (lastDir == 0) {
                anim.SetTrigger("Run");
                anim.speed = 1;
                dirArray.Clear();
            } else if (lastDir == 3) {
                anim.SetTrigger("Run");
                anim.speed = 1;
                dirArray.Clear();
            } else if (lastDir == 1) {
                anim.SetTrigger("Run");
                anim.speed = 1;
                dirArray.Clear();
            } else if (lastDir == 2) {
                anim.SetTrigger("Run");
                anim.speed = 1;
                dirArray.Clear();
            }*/
        }
    }
}