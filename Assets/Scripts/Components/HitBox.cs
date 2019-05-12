using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallback;

namespace Comps
{
    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    };

    public class HitBox : MonoBehaviour
    {
        public bool useSphere = false;

        public LayerMask HitBoxMasks;

        public Vector3 hitboxSize = Vector3.one;

        public float radius = 0.5f;

        public Color inactiveColor, collisionOpenColor, collidingColor;

        private ColliderState state;

        private void Start()
        {
            //Init the collider to inactive when object is spawned
            state = ColliderState.Closed;
        }

        private void Update()
        {
            if (state == ColliderState.Closed) { return; }

            Collider2D[] coll;

            if (useSphere)
            {
                coll = Physics2D.OverlapCircleAll(transform.position, radius, HitBoxMasks);
            }
            else
            {
                coll = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0, HitBoxMasks);
            }

            if (coll.Length > 0)
            {
                state = ColliderState.Colliding;
                // We should do something with the colliders
                Debug.Log("There was a hit");

                //    //Start the call for the damage Event system
                //    DamageEvent damageEventInfo = new DamageEvent();
                //    //Since the hitbox is a child of the attacker object we need to return the parent object to the event system
                //    damageEventInfo.baseGO = transform.parent.gameObject;
                //    damageEventInfo.targetGO = coll.gameObject;
                //    damageEventInfo.FireEvent();
            }
            else
            {
                state = ColliderState.Open;
            }
        }


        public void startCheckingCollision()
        {
            state = ColliderState.Open;
        }

        public void stopCheckingCollision()
        {
            state = ColliderState.Closed;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);

            switch (state)
            {
                case ColliderState.Closed:
                    Gizmos.color = inactiveColor;
                    break;

                case ColliderState.Open:
                    Gizmos.color = collisionOpenColor;
                    break;

                case ColliderState.Colliding:
                    Gizmos.color = collidingColor;
                    break;
            }

            if (useSphere)
            {
                Gizmos.DrawWireSphere(transform.position, radius);
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, hitboxSize);
            }
        }
    }
}