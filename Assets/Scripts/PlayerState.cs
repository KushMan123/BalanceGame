using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Newspaper, Wood, Cement }
public class PlayerState : MonoBehaviour
{

    public State playerState = State.Wood;
    public PlayerController playerController;

    private void Update()
    {
        changeMaterial();
    }
    // Start is called before the first frame update
    public void setState(State newState)
    {
        playerState = newState;
    }

    public State getState()
    {
        return playerState;
    }

    void changeMaterial()
    {
        switch (playerState)
        {
            case State.Newspaper:
                this.GetComponent<Renderer>().material.color = new Color(0.89f, 0.03f, 0.03f);
                playerController.rigidBody.mass = 2;
                break;
            case State.Wood:
                this.GetComponent<Renderer>().material.color = new Color(0f, 0.73f, 1f);
                playerController.rigidBody.mass = 5;
                break;
            case State.Cement:
                this.GetComponent<Renderer>().material.color = new Color(0.03f, 0.88f, 0.22f);
                playerController.rigidBody.mass = 10;
                break;
        }
    }

    }
