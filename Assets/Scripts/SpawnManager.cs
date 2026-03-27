using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public Rigidbody box;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 1f, 2f);
        //StartCoroutine(Hello());
        //StartCoroutine(Goodbye());
        StartCoroutine(SpawnRoutine());

    }

    private void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }

    void RandomSpawn()
    {
        int Index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[Index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }


        IEnumerator Hello()
        {
            Debug.Log("Hello " + Time.frameCount);
            yield return null;
        }

        IEnumerator Goodbye()
        {
            while (true)
            {

                Debug.Log("HGoodBye " + Time.frameCount + " " + Time.time);
                //yield return new WaitForSeconds(1);
                yield return null;
                yield break;
                //StartCoroutine(Hello());               
            }
        }
        IEnumerator Movebox()
        {
            box.linearVelocity = 10 * Vector3.up;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.right;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.down;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.left;
            yield return new WaitForSeconds(3);
        }
    }

