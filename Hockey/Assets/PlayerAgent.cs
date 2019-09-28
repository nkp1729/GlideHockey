using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;


public class PlayerAgent : Agent
{
    public GameObject puck;
    public bool invertX;
    public int score;
    public GameObject myRegion;

    private Text textComponent;
    private Rigidbody agentRb;
    private Rigidbody puckRb;
    private float invertMult;

    // Looks for the scoreboard based on the name of the gameObjects.
    // Do not modify the names of the Score GameObjects
    private const string CanvasName = "Canvas";
    private const string ScoreBoardAName = "ScoreA";
    private const string ScoreBoardBName = "ScoreB";

    public override void InitializeAgent()
    {
        agentRb = GetComponent<Rigidbody>();
        puckRb = puck.GetComponent<Rigidbody>();
        var canvas = GameObject.Find(CanvasName);
        GameObject scoreBoard;
        if (invertX)
        {
            scoreBoard = canvas.transform.Find(ScoreBoardBName).gameObject;

        }
        else
        {
            scoreBoard = canvas.transform.Find(ScoreBoardAName).gameObject;
        }
        textComponent = scoreBoard.GetComponent<Text>();

    }

    public override void CollectObservations()
    {
        AddVectorObs(invertMult * (transform.position.x - myRegion.transform.position.x));
        AddVectorObs(transform.position.z - myRegion.transform.position.z);
        AddVectorObs(invertMult * agentRb.velocity.x);
        AddVectorObs(agentRb.velocity.z);

        AddVectorObs(invertMult * (puck.transform.position.x - myRegion.transform.position.x));
        AddVectorObs(puck.transform.position.z - myRegion.transform.position.z);
        AddVectorObs(invertMult * puckRb.velocity.x);
        AddVectorObs(puckRb.velocity.z);
    }


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var moveX = Mathf.Clamp(vectorAction[0], -1f, 1f) * invertMult;
        var moveZ = Mathf.Clamp(vectorAction[1], -1f, 1f);

            //if (moveY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
            //{
            //    agentRb.velocity = new Vector3(agentRb.velocity.x, 2f, 0f);
            //}

        agentRb.velocity = new Vector3(moveX * 3f, 0f, moveZ*3f);

        if (!invertX && transform.position.x - transform.parent.transform.position.x < -5.0f*invertMult ||
            invertX && transform.position.x - transform.parent.transform.position.x > 5.0f*invertMult)
        {
            transform.position = new Vector3(-invertMult + transform.parent.transform.position.x,
                                                        transform.position.y,
                                                        transform.position.z);
        }

        textComponent.text = score.ToString();
    }

    public override void AgentReset()
    {
        invertMult = invertX ? -1f : 1f;

        transform.position = new Vector3(-invertMult * Random.Range(-2f, -4f), 0.3f, 0f); 
        //+ transform.parent.transform.position;
        agentRb.velocity = new Vector3(0f, 0f, 0f);
    }
}
