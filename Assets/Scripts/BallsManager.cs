using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallsManager : MonoBehaviour
{
    #region  Singleton
    
    private  static BallsManager _instance;

    public static BallsManager Instance => _instance;

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } 
        else
        {
            _instance = this;
        }

    }
    
    #endregion
    
    [SerializeField]
    private Ball ballPrefab;
    
    private Ball InitialBall;

    private Rigidbody2D InitialBallRb;

    public float InitialBallSpeed = 250;

    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            //Align ball positon to the paddle position
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
            InitialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                InitialBallRb.isKinematic = false;
                InitialBallRb.AddForce(new Vector2(0, InitialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        InitBall();
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
        InitialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        InitialBallRb = InitialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            InitialBall
        };
    }
}
