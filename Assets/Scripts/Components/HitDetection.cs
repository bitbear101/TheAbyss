﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallback;

namespace Comps
{
    public class HitDetection : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D coll)
        {
            //NOTE: Really need a better way to check for the collisions

            //Start the call for the damage Event system
            DamageEvent damageEventInfo = new DamageEvent();
            //Since the hitbox is a child of the attacker object we need to return the parent object to the event system
            damageEventInfo.baseGO = transform.parent.gameObject;
            damageEventInfo.targetGO = coll.gameObject;
            damageEventInfo.FireEvent();
        }

        private void OnTriggerStay2D(Collider2D coll)
        {
            Debug.Log("Trigger stay - " + coll.name);
        }
    }
}