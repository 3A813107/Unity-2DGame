using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TestEnemyAI : MonoBehaviour
{

    public float Speed;
    public List<Transform> points;
    public int nextID=0;

    int idChangeValue = 1;
    private PlayerHealth playerHealth;

    public int health;
    public int damage;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger=true;
        GameObject root = new GameObject(name+"_Root");
        root.transform.position =transform.position;
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position=root.transform.position;
        GameObject p1 = new GameObject("Point1");p1.transform.SetParent(waypoints.transform);p1.transform.position=root.transform.position;
        GameObject p2 = new GameObject("Point2");p2.transform.SetParent(waypoints.transform);p2.transform.position=root.transform.position;
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }
    void Start()
    {
        playerHealth=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        Transform goalPoint = points[nextID];
        if(goalPoint.transform.position.x>transform.position.x)
        {
            transform.localScale=new Vector3(-3,3,1);
        }
        else
        {
            transform.localScale=new Vector3(3,3,1);
        }
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,Speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,goalPoint.position)<1f)
        {
            if(nextID == points.Count-1)
            {
                idChangeValue=-1;
            }
            if(nextID == 0)
            {
                idChangeValue=1;
            }
            nextID += idChangeValue;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if(playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
    
}
