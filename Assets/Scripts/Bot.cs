using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float force;
    [SerializeField] Transform ball;
    [SerializeField] List<Transform> aimTargets;

    Animator botAnim;
    Vector3 targetPos;

    void Start()
    {
        botAnim = GetComponent<Animator>();
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        targetPos.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            int randomAimTarget = Random.Range(0, aimTargets.Count);
            Vector3 dir = aimTargets[randomAimTarget].position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);

            AudioManager.Instance.PlayTennisSound();

            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x >= 0)
            {
                botAnim.Play("righthand");
            }
            else
            {
                botAnim.Play("lefthand");
            }
            ball.GetComponent<Ball>().Hitter = "Bot";
        }
    }
}
