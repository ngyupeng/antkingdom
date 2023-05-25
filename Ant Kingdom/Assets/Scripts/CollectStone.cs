using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CollectStone : MonoBehaviour
{
    public Transform target;
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
        ResourcePanelButton.onButtonClicked += SetTarget;
    }

    void GetPath()
    {
        if (seeker.IsDone())
             seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    public void SetTarget() {
        target = ResourceNode.selectedNode.transform;
        Invoke("GetPath",0f);
        Invoke("UpdateResource", 5f);
    }


    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdateResource()
    {
        GameResources.ResourceType type = ResourceNode.selectedNode.getName() == "Stone"
                    ? GameResources.ResourceType.Stone 
                    : GameResources.ResourceType.Wood;
        GameResources.AddResourceAmount(
                 type, ResourceNode.selectedNode.getAmount());
        if (type == GameResources.ResourceType.Wood)
        {
            Debug.Log("Wood Amount: " + GameResources.GetResourceAmount(type));
        } else
        {
            Debug.Log("Stone Amount: " + GameResources.GetResourceAmount(type));
        }
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
