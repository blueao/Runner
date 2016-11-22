using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
public class CharacterController : MonoBehaviour
{

    public Rigidbody2D rigidbody;

    float maxJumpHeight = 3.0f;
    float groundHeight;
    public Vector3 groundPos;
    public readonly float jumpSpeed = 100f;
    public readonly float fallSpeed = 1f;
    public bool inputJump = false;
    public bool grounded = false;
    void Start()
    {
        groundPos = new Vector3(transform.position.x, transform.position.y+100,transform.position.z);
        groundHeight = transform.position.y;
        maxJumpHeight = transform.position.y + maxJumpHeight;
    }
    public void jumb()
    {
       // groundPos = new Vector3(transform.position.x, transform.position.y + 200, transform.position.z);
        inputJump = true;
      
        //rigidbody.AddForce(Vector3.up * 500f, ForceMode2D.Impulse);
    }
    IEnumerator  Jump()
    {
        while (true)
        {
            if (transform.position.y >= maxJumpHeight)
                inputJump = false;
            if (inputJump)
                transform.Translate(Vector3.up * jumpSpeed * Time.fixedDeltaTime);
            else if (!inputJump)
            {
                transform.position = Vector3.Lerp(transform.position, groundPos, fallSpeed * Time.fixedDeltaTime);
                if (transform.position.y >= groundPos.y-10f)
                {
                    transform.GetComponent<Rigidbody2D>().gravityScale =50;
                    transform.GetComponent<Rigidbody2D>().isKinematic = false;
                    StopAllCoroutines();
                }
            }

           yield return new WaitForEndOfFrame();

        }
    }
}
