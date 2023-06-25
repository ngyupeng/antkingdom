using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CollectResource : MonoBehaviour
{
    public Transform target;
    public ResourceNode targetNode;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rb; 
    // Start is called before the first frame update
    void Awake()
    {
        target = transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void GetPath()
    {
        if (seeker.IsDone())
             seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    public void SetTarget(GameObject targetObject) {
        target = targetObject.transform;
        Invoke("GetPath",0f);
        Invoke("UpdateResource", 5f);
    }


    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            Debug.Log("Pathcomplete");
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdateResource()
    {
        Resource resource = targetNode.GetResource();
        GameResources.ResourceType type = resource.GetResourceType();
        GameResources.AddResourceAmount(
                 type, targetNode.TakeAmount(10));
        Debug.Log(resource.GetName() + " Amount: " + GameResources.GetResourceAmount(type));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else 
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
           

    }
}
