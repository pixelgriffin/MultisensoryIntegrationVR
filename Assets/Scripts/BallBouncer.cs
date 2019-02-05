using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBouncer : MonoBehaviour {

    public Transform leftBall, rightBall;
    public Transform leftTo, rightTo;

    public AudioSource hitSound;

    private float timeToHit;
    private float timeToSound;

    public InputField secondsHitField;
    public InputField secondsSoundField;
    public InputField ballDistance;

    private Vector3 leftStart, rightStart;
    private float ht, st;
    private bool playedSound = false;
    private bool play = false;

    private float ballDist = 0f;

	void Start () {
        secondsHitField.text = "" + 1;
        secondsSoundField.text = "" + 1;

        timeToHit = 1f;
        timeToSound = 1f;

        leftStart = leftBall.position;
        rightStart = rightBall.position;

        timeToHit *= 2f;
	}
	
	void Update () {
        if(!float.TryParse(secondsHitField.text, out timeToHit))
        {
            timeToHit = 1f;
            secondsHitField.text = "" + 1;
        }
        if(!float.TryParse(secondsSoundField.text, out timeToSound))
        {
            timeToSound = 1f;
            secondsSoundField.text = "" + 1;
        }
        if (!float.TryParse(ballDistance.text, out ballDist))
        {
            ballDist = 1f;
            ballDistance.text = "" + 1;
        }

        leftStart = this.transform.position + new Vector3(-ballDist / 2f, ballDist / 2f, 0f);
        rightStart = this.transform.position + new Vector3(ballDist / 2f, ballDist / 2f, 0f);

        leftTo.localPosition = new Vector3(ballDist / 2f, -ballDist / 2f, 0f);
        rightTo.localPosition = new Vector3(-ballDist / 2f, -ballDist / 2f, 0f);

        if (play)
        {
            ht += Time.deltaTime / (timeToHit * 2);
            st += Time.deltaTime;

            leftBall.transform.position = Vector3.Lerp(leftStart, leftTo.transform.position, ht);
            rightBall.transform.position = Vector3.Lerp(rightStart, rightTo.transform.position, ht);

            if (st > timeToSound && !playedSound)
            {
                hitSound.Play();
                playedSound = true;
            }
        }
	}

    public void ResetBalls()
    {
        play = false;
        playedSound = false;
        st = 0f;
        ht = 0f;
        leftBall.position = leftStart;
        rightBall.position = rightStart;
    }

    public void PlayBalls()
    {
        play = true;
    }
}
