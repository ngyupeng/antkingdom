using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CollectResource : MonoBehaviour
{
    private enum State {
        Idle,
        MovingToResourceNode,
        GatheringResources,
        MovingToBase
    }
    private State state;
    public Transform target;
    public ResourceNode targetNode;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isIdle = true;
    
    Seeker seeker;
    Rigidbody2D rb; 
    // Start is called before the first frame update
    void Awake()
    {
        state = State.Idle;
        target = transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void GetPath()
    {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void SetTargetNode(ResourceNode node) {
        targetNode = node;
        SetTarget(node.gameObject);
        state = State.MovingToResourceNode;
    }
    public void SetTarget(GameObject targetObject) {
        isIdle = false;
        target = targetObject.transform;
        Invoke("GetPath",0f);
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
        Resource resource = targetNode.GetResource();
        GameResources.ResourceType type = resource.GetResourceType();
        // Check how much it can take, then take as much as the capacity allows
        int amount = targetNode.CanTakeAmount(10);
        targetNode.TakeAmount(GameResources.AddResourceAmount(type, amount));
        isIdle = true;
    }

    void Update() {
        switch (state) {
            case State.Idle:
                break;
            case State.MovingToResourceNode:
                if (isIdle) {
                    isIdle = false;
                    Invoke("UpdateResource", 2f);
                    state = State.GatheringResources;
                }
                break;
            case State.GatheringResources:
                if (isIdle) {
                    SetTarget(AntSpawner.instance.nest.gameObject);
                    state = State.MovingToBase;
                }
                break;
            case State.MovingToBase:
                if (isIdle) {
                    AntManager.AddIdleAnt(AntManager.AntType.WorkerAnt);
                    Destroy(gameObject);
                }
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count || Vector2.Distance(rb.position, target.transform.position) <= 1f)
        {
            reachedEndOfPath = true;
            path = null;
            isIdle = true;
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
