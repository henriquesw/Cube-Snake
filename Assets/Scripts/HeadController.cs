using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

    public GameObject Tail;
    public GameObject TailSegment;

    public GameObject tutorialRight;
    public GameObject tutorialLeft;
    private bool tutorialR = false;
    private bool tutorialL = false;

    public bool move = true;
    private Stats stats;
    private bool beingHandled = false, lost = false;
    private Vector3 fp, lp;
    private int count = 0;
    private int turn = 0;
    private bool isPaused = false;

    public void Start()
    {
        if (PlayerPrefs.GetInt("HighScore") == 0 && PlayerPrefs.GetInt("Input") == InputType.Swipe)
        {
            tutorialR = true;
            tutorialRight.SetActive(true);
        }
        stats = GetComponentInParent<Stats>();
    }

    public void Update()
    {
        if (!isPaused)
        {
            if (!beingHandled && !lost)
            {
                StartCoroutine(HandleIt());
            }
            Movement();
        }
        if(Vector3.Distance(transform.position, Vector3.zero) > 12 && !lost)
        {
            lost = true;
            GameObject.Find("Canvas").GetComponent<UIBehaviour>().Lose();
        }
    }

    private void Movement()
    {
        if (move && !lost)
        {
            if (Input.GetKeyDown(KeyCode.A) && move)
            {
                turn = -1;
            }

            else if (Input.GetKeyDown(KeyCode.D) && move)
            {
                turn = 1;
            }
            if (PlayerPrefs.GetInt("Input") == InputType.Tap)
            {
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.y < Screen.height - (Screen.height / 5))
                        {
                            if (touch.position.x > Screen.width / 2)
                            { //Right tap
                                turn = 1;
                            }
                            else if (touch.position.x < Screen.width / 2)
                            { //Left tap
                                turn = -1;
                            }
                        }
                    }
                }
            }
            else if (PlayerPrefs.GetInt("Input") == InputType.Swipe)
            {
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        fp = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        lp = touch.position;
                        if (Mathf.Abs(lp.x - fp.x) > 50)
                        {
                            if (lp.x > fp.x)
                            {   //Right swipe
                                turn = 1;
                                if (tutorialR)
                                {
                                    tutorialRight.SetActive(false);
                                    tutorialLeft.SetActive(true);
                                    tutorialR = false;
                                    tutorialL = true;
                                }
                            }
                            else
                            {   //Left swipe
                                turn = -1;
                                if (tutorialL)
                                {
                                    tutorialLeft.SetActive(false);
                                    tutorialL = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            addTail();
            stats.Points++;
            stats.Speed += stats.Acceleration;
            GetComponent<AudioSource>().Play();
        }
        if (other.CompareTag("Tail"))
        {
            lost = true;
            GameObject.Find("Canvas").GetComponent<UIBehaviour>().Lose();
            int timesPlayed = PlayerPrefs.GetInt("TimesPlayed");
            timesPlayed++;
            if(timesPlayed == 3)
            {
                GameObject.Find("AdManager").GetComponent<AdManager>().ShowAd();
                timesPlayed = 0;
            }
            PlayerPrefs.SetInt("TimesPlayed", timesPlayed);
        }
    }

    private IEnumerator HandleIt()
    {
        beingHandled = true;

        if(turn == 1 && move)
        {
            transform.Rotate(Vector3.up, 90);
            StartCoroutine(RotateCamera(-90, 0));
            turn = 0;
        }
        else if(turn == -1 && move)
        {
            transform.Rotate(Vector3.up, -90);
            StartCoroutine(RotateCamera(90, 0));
            turn = 0;
        }

        moveTail(transform.position);
        transform.Translate(Vector3.forward);

        yield return new WaitForSeconds(1/stats.Speed);
        beingHandled = false;
    }

    public void moveTail(Vector3 position)
    {
        GameObject tail = Tail;
        while(tail != null)
        {
            Vector3 tempPosition = tail.transform.position;
            tail.transform.position = position;
            position = tempPosition;
            tail = tail.GetComponent<TailController>().Tail;
        }
    }

    public void addTail()
    {
        GameObject tail = Tail;
        while (tail.GetComponent<TailController>().Tail != null)
        {
            tail = tail.GetComponent<TailController>().Tail;
        }
        tail.GetComponent<TailController>().Tail = Instantiate(TailSegment, tail.transform.position, Quaternion.identity);
        GameObject temp = tail.GetComponent<TailController>().Tail;
        temp.GetComponent<TailController>().Leader = tail;
        temp.transform.SetParent(GameObject.Find("Snake").transform);
        temp.GetComponent<Renderer>().material = GameObject.Find("Snake").GetComponent<SkinManager>().NextMaterial();
        count++;
    }

    public IEnumerator RotateCamera(float angle, int direction)
    {
        angle = angle / 5;
        for (int i = 0; i < 5; i++)
        {
            if (direction == 0)
            {
                Camera.main.transform.Rotate(Vector3.forward, angle);
            }
            else if (direction == 1)
            {
                Camera.main.transform.RotateAround(Vector3.zero, transform.right, angle);
            }
            yield return new WaitForSeconds((1/stats.Speed)/5);
        }
    }
}
