using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] VariableJoystick joystick;
    [SerializeField] float speed = 3f;
    [SerializeField] float force = 13f;

    [SerializeField] Transform aimTarget;
    [SerializeField] Transform ball;

    [SerializeField] Transform servePos;

    private Animator playerAnim;
    private Vector3 aimTartgetInitialPosition;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        aimTartgetInitialPosition = aimTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (aimTarget.transform.position.x <= -3f || aimTarget.transform.position.x >= 3f)
        {
            aimTarget.transform.position = aimTartgetInitialPosition;
        }
        else
        {
            aimTarget.Translate(new Vector3(-horizontalInput, 0, 0) * speed * Time.deltaTime);

        }

        if ((horizontalInput != 0 || verticalInput != 0))
        {
            Vector3 moveDir = transform.right * horizontalInput + transform.forward * verticalInput;
            transform.Translate(-moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);

            AudioManager.Instance.PlayTennisSound();

            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x >= 0)
            {
                playerAnim.Play("righthand");
            }
            else
            {
                playerAnim.Play("lefthand");
            }

            ball.GetComponent<Ball>().Hitter = "Player";
        }
    }

    public void Reset()
    {
        transform.position = servePos.position;
    }
}
