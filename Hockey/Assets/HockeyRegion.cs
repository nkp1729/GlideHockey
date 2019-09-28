using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class HockeyRegion : MonoBehaviour
{

    public GameObject puck;
    public GameObject agentA;
    public GameObject agentB;
    private Rigidbody puckRb;

    // Start is called before the first frame update
    void Start()
    {
        puckRb = puck.GetComponent<Rigidbody>();
        MatchReset();
        puckRb.velocity = new Vector3(2.0f, 0.0f, 0.1f);

    }


    public void MatchReset()
    {
        float puckOut = Random.Range(1f, 2f);
        int flip = Random.Range(0, 2);
        if (flip == 0)
        {
            puck.transform.position = new Vector3(0f, 0.1f, 0f);
        }
        else
        {
            puck.transform.position = new Vector3(0f, 0.1f, 0f);
        }
        puckRb.velocity = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
        puck.transform.localScale = new Vector3(1, 0.1f, 1);
        puck.GetComponent<HitGoal>().lastAgentHit = -1;
    }

    void FixedUpdate()
    {
        Vector3 rgV = puckRb.velocity;
        puckRb.velocity = new Vector3(Mathf.Clamp(rgV.x, -9f, 9f), Mathf.Clamp(rgV.y, -9f, 9f), rgV.z);
    }
}