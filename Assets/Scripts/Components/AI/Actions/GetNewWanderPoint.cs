﻿using Comps;
using UnityEngine;

[CreateAssetMenu(menuName = "Comps/AI/Actions/New Wander Point")]
public class GetNewWanderPoint : Action
{
    public override void Act(AI controller)
    {
        NewWanderPoint(controller);
    }

    private void NewWanderPoint(AI controller)
    {
        if (controller.gotNewDirection == true) return;
   
            Vector2 newPatrolPoint;

            while (true)
            {
                newPatrolPoint = new Vector3(Mathf.RoundToInt(Random.Range(-(controller.wanderDistance + 1), controller.wanderDistance) + controller.gameObject.transform.position.x), Mathf.RoundToInt(Random.Range(-(controller.wanderDistance + 1), controller.wanderDistance) + controller.gameObject.transform.position.y),0f);

                Vector2 dirToRaycast = (newPatrolPoint - new Vector2(controller.gameObject.transform.position.x, controller.gameObject.transform.position.x)).normalized;

                if (!Physics2D.Linecast(controller.rb2d.position, newPatrolPoint, controller.obstacleMask) && Vector2.Angle(controller.gameObject.transform.right, dirToRaycast) < controller.viewAngle / 2)
                {
                    controller.gotNewDirection = true;
                    controller.wanderPoint = newPatrolPoint;
                    break;
                }
            }
    }
}