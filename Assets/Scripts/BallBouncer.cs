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

    private Vector3 leftStart, rightStart;
    private float ht, st;
    private bool playedSound = false;
    private bool play = false;

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
