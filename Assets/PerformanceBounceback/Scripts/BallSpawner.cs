using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public static BallSpawner current;

    public GameObject pooledBall; //the prefab of the object in the object pool
    public int ballsAmount = 20; //the number of objects you want in the object pool
    public List<GameObject> pooledBalls; //the object pool
    public static int ballPoolNum = 0; //a number used to cycle through the pooled objects

    private float cooldown;
    private float cooldownLength = 0.5f;

    void Awake()
    {
        current = this; //makes it so the functions in ObjectPool can be accessed easily anywhere
    }

    void Start()
    {
        //Create Bullet Pool
        pooledBalls = new List<GameObject>();
        for (int i = 0; i < ballsAmount; i++)
        {
            GameObject obj = Instantiate(pooledBall);
            obj.SetActive(false);
            pooledBalls.Add(obj);
        }
    }

public GameObject GetPooledBall()
{
    ballPoolNum++;
        /*  if (ballPoolNum > (ballsAmount - 1))
          { ballPoolNum = 0; }*/
        if (ballPoolNum >= pooledBalls.Count)
        { // Reset it to 0 if the number is larger than the number of objects we have in the pool.
            ballPoolNum = 0;
        }

       if (pooledBalls[ballPoolNum].activeSelf)
        {
            foreach (var ball in pooledBalls) {
                // If the object is not active... 
                if (!ball.activeSelf)
                { // return that one. 
                    return ball;
                }
            }
            // If it gets down here, we have no available objects in the pool.
            GameObject newBall = Instantiate(pooledBall);
            pooledBalls.Add(newBall);
            newBall.SetActive(false);
            return newBall;
        }
        else
        {
            return pooledBalls[ballPoolNum];
        }
        //if we’ve run out of objects in the pool too quickly, create a new one
        /* if (pooledBalls[ballPoolNum].activeInHierarchy)
    {
        //create a new bullet and add it to the Pooled Ball List
        GameObject obj = Instantiate(pooledBall);
        pooledBalls.Add(obj);
        ballsAmount++;
        ballPoolNum = ballsAmount - 1;
    }
      //  Debug.Log("GetPooledBall returned ball number: " + ballPoolNum);
        return pooledBalls[ballPoolNum];
        */
    }

    // Update is called once per frame
    void Update () {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            cooldown = cooldownLength;
            SpawnBall();
        }		
	}

    void SpawnBall()
    {
        GameObject selectedBall = BallSpawner.current.GetPooledBall();
        // Set ball to active. 
        selectedBall.SetActive(true);

        // Choose new position in the scene.
        selectedBall.transform.position = transform.position;
        Rigidbody selectedRigidbody = selectedBall.GetComponent<Rigidbody>();
        selectedRigidbody.velocity = Vector3.zero;
        selectedRigidbody.angularVelocity = Vector3.zero;
        // Call new coroutine method
        StartCoroutine(TemporaryBall(selectedBall));

       /* GameObject selectedBall = BallSpawner.current.GetPooledBall();
        selectedBall.transform.position = transform.position;
        Rigidbody selectedRigidbody = selectedBall.GetComponent<Rigidbody>();
        selectedRigidbody.velocity = Vector3.zero;
        selectedRigidbody.angularVelocity = Vector3.zero;
        selectedBall.SetActive(true);*/
    }
    public IEnumerator TemporaryBall(GameObject ball)
    {
        // After a period of time, destroy the coin. 
        yield return new WaitForSeconds(10);
        ball.SetActive(false);
    }

}
