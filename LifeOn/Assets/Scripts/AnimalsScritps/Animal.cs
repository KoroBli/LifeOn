using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour
{
	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshhold = .5f;

	[SerializeField] private Transform target;
	public float wanderTimeRest = 5;
	public float speed = 20;
	public float turnDst = 5;
	public float turnSpeed = 3;
	public float stoppingDst = 10;

	bool isWandering = true;

	Path path;

	void Start()
	{
		if (target != null)
		{
			StartCoroutine("UpdatePath");
		}
	}

    private void Update()
    {
        if(isWandering)
        {
			wanderTimeRest += Time.deltaTime;

			if (wanderTimeRest >= 5)
			{
				StartCoroutine("Wandering");
				wanderTimeRest = 0;
			}
        }
    }

	IEnumerator Wandering()
    {
		yield return null;
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath()
    {
		if (target != null)
		{
			if (Time.timeSinceLevelLoad < .3f)
			{
				yield return new WaitForSeconds(.3f);
			}
			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

			float sqrMoveThreshhold = pathUpdateMoveThreshhold * pathUpdateMoveThreshhold;
			Vector3 targetPosOld = target.position;

			while (true)
			{
				yield return new WaitForSeconds(minPathUpdateTime);
				if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshhold)
				{
					PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
					targetPosOld = target.position;
				}
			}
		}
    }

	IEnumerator FollowPath()
	{
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
				if(pathIndex == path.finishLineIndex)
                {
					followingPath = false;
					break;
                }
				else
                {
					pathIndex++;
                }
            }

			if(followingPath)
            {
				if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
				{
					speedPercent = path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst;
					if(speedPercent < 0.01f)
                    {
						followingPath = false;
                    }
				}

				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			path.DrawWithGizmos();
		}
	}
}