using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class Raycaster
    {
        private Transform _startTransform;
        private Vector3 _direction;
        private LayerMask _layerMask;
        private float _rayLength;
        private Collider _previous;
        private RaycastHit _hit;
        
        public event Action<Collider, Raycaster> RayEnter;
        public event Action<Collider, Raycaster> RayStay;
        public event Action<Collider, Raycaster> RayExit;

        public Raycaster(Transform startTransform, Vector3 direction, float rayLength, LayerMask layerMask)
        {
            _startTransform = startTransform;
            _direction = direction;
            _rayLength = rayLength;
            _layerMask = layerMask;
        }
        
        public void Cast()
        {
            Physics.Raycast(_startTransform.position, _direction, out _hit, _rayLength, _layerMask);
            Debug.DrawRay(_startTransform.position, _direction, Color.green, 2);
            ProcessCollision(_hit.collider, this);
        }

        private void ProcessCollision(Collider current, Raycaster raycaster)
        {
            // No collision this frame.
            if (current == null)
            {
                // But there was an object hit last frame.
                if (_previous != null)
                {
                    Invoke(RayExit, _previous, raycaster);
                }
            }

            // The object is the same as last frame.
            else if (_previous == current)
            {
                Invoke(RayStay, current, raycaster);
            }

            // The object is different than last frame.
            else if (_previous != null)
            {
                Invoke(RayExit, _previous,raycaster);
                Invoke(RayEnter, current,raycaster);
            }

            // There was no object hit last frame.
            else
            {
                Invoke(RayEnter, current,raycaster);
            }

            // Remember this object for comparing with next frame.
            _previous = current;
        }
        
        private void Invoke(Action<Collider, Raycaster> action, Collider collider, Raycaster raycaster)
        {
            action?.Invoke(collider, raycaster);
        }
    }
}