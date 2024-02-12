using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : EntityWithTimer
    {
        public enum AIState
        {
            Idle,
            PatrolArea,
            PatrolPath,
        }

        public enum AIFollow
        {
            Simple,
            Prediction
        }

        [SerializeField] private SpaceShip _Ship;

        [SerializeField] private float DetectObstacleDistance = 4f;

        [SerializeField] private AIState _State;
        [SerializeField] private AIState? _PrevState;

        [SerializeField] private AIFollow _Follow;

        [SerializeField] private Destructable _Target;
        [SerializeField] private Vector2 _Destination;

        [SerializeField] private Vector2 _PatrolAreaCenter;
        [SerializeField] private float _PatrolAreaRadius = 16.0f;
        private string _DestimationChangeTimerName = "destchange";
        private float _DestimationChangeTimerValue = 10.0f;

        [SerializeField] private List<Vector2> _PatrolPath;
        [SerializeField] private int _PatrolPathLength = 3;
        [SerializeField] private int _CurrentPoint = 0;

        [SerializeField] private bool _IsAbleToFire;
        [SerializeField] private float _FireDelay = 1f;
        private string _FireDelayTimerName = "firedelay";

        private float _TargetFindTimerValue = 0.5f;
        private string _TargetFindTimerName = "targetfind";

        [SerializeField] private float _DetectionRange = 4.0f;
        [SerializeField] private float _ScopeMaxAngle = 10.0f;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _MaxThrust;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _MaxTorque;

        [SerializeField] private float _Thrust;
        [SerializeField] private float _Torque;

        void Start()
        {
            _Ship = GetComponent<SpaceShip>();
            _PrevState = null;
        }

        protected void HandleStateChange()
        {
            if (_PrevState == _State)
            {
                return;
            }

            switch (_PrevState)
            {
                case AIState.Idle:
                    break;
                case AIState.PatrolArea:
                    _Target = null;
                    RemoveTimer(_FireDelayTimerName);
                    RemoveTimer(_DestimationChangeTimerName);
                    RemoveTimer(_TargetFindTimerName);
                    break;
                case AIState.PatrolPath:
                    _Target = null;
                    RemoveTimer(_FireDelayTimerName);
                    RemoveTimer(_TargetFindTimerName);
                    break;
            }

            switch (_State)
            {
                case AIState.Idle:
                    break;
                case AIState.PatrolArea:

                    _Destination = Vector2.zero;
                    AddTimer(_DestimationChangeTimerName, _DestimationChangeTimerValue);
                    AddCallback(_DestimationChangeTimerName, PatrolAreaNewDestination);

                    AddTimer(_FireDelayTimerName, _FireDelay);
                    AddCallback(_FireDelayTimerName, Reload);

                    AddTimer(_TargetFindTimerName, _TargetFindTimerValue);
                    AddCallback(_TargetFindTimerName, FindTarget);

                    break;
                case AIState.PatrolPath:

                    _Destination = Vector2.zero;
                    AddTimer(_FireDelayTimerName, _FireDelay);
                    AddCallback(_FireDelayTimerName, Reload);

                    AddTimer(_TargetFindTimerName, _TargetFindTimerValue);
                    AddCallback(_TargetFindTimerName, FindTarget);

                    if (_PatrolPath == null)
                    {
                        _PatrolPath = new List<Vector2>();
                    }
                    _CurrentPoint = 0;

                    break;
            }

            _PrevState = _State;
        }

        private void Reload()
        {
            _IsAbleToFire = true;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleStateChange();

            switch (_State)
            {
                case AIState.Idle:
                    _Thrust = 0;
                    _Torque = 0;
                    break;
                case AIState.PatrolArea:
                    PatrolAreaActions();
                    break;
                case AIState.PatrolPath:
                    PatrolPathActions();
                    break;
            }
        }

        void PatrolAreaActions()
        {
            if (_PatrolAreaCenter == null)
            {
                _PatrolAreaCenter = _Ship.transform.position;
            }

            if (_Destination == Vector2.zero)
            {
                PatrolAreaNewDestination();
            }

            PatrolAreaCalcCourse();
            AvoidObstacle();
            ShipControls();
            Fire();
        }

        void PatrolPathActions()
        {
            if (_PatrolPath.Count == 0)
            {
                RegeneratePath();
            }

            PatrolPathCalcCourse();
            AvoidObstacle();
            ShipControls();
            Fire();
        }

        void Fire()
        {
            Vector2 localCoordinatesDest = _Ship.transform.InverseTransformPoint(_Destination);
            float angleToDestination = Vector3.SignedAngle(localCoordinatesDest, Vector3.up, Vector3.forward);
            bool targetInScope = Math.Abs(angleToDestination) < _ScopeMaxAngle;

            if (_Target != null && _IsAbleToFire && targetInScope)
            {
                _Ship.Fire(TurretMode.Primary);
                _IsAbleToFire = false;
            }
        }

        void FindTarget()
        {
            if (_Target != null)
            {
                return;
            }

            float minSqrDistance = 10000;
            float sqrDetectionRange = _DetectionRange * _DetectionRange;
            foreach (var target in Destructable.AllDestructables)
            {
                if (target == _Ship)
                {
                    continue;
                }

                if (target.FractionId == 0)
                {
                    continue;
                }

                if (target.FractionId == _Ship.FractionId)
                {
                    continue;
                }

                float sqrDistance = (_Ship.transform.position - target.transform.position).sqrMagnitude;

                if (sqrDistance > sqrDetectionRange)
                {
                    continue;
                }

                if (sqrDistance > minSqrDistance)
                {
                    continue;
                }

                minSqrDistance = sqrDistance;
                _Target = target;
            }
        }

        void RegeneratePath()
        {
            _PatrolPath.Clear();
            for (int i = 0; i < _PatrolPathLength; i++)
            {
                _PatrolPath.Add(getRandomPointInCircle(_PatrolAreaCenter, _PatrolAreaRadius));
            }
        }

        private void AvoidObstacle()
        {
            RaycastHit2D hit = Physics2D.Raycast(_Ship.transform.position, _Ship.transform.up, DetectObstacleDistance);
            RaycastHit2D hitLeft45 = Physics2D.Raycast(_Ship.transform.position, _Ship.transform.up - _Ship.transform.right, DetectObstacleDistance * 0.6f);
            RaycastHit2D hitRight45 = Physics2D.Raycast(_Ship.transform.position, _Ship.transform.up + _Ship.transform.right, DetectObstacleDistance * 0.6f);
            Rigidbody2D ship = _Ship.GetComponent<Rigidbody2D>();

            if ((hit || hitLeft45 || hitRight45) && _Target != null)
            {
                RaycastHit2D thit = hit
                    ? hit
                    : hitLeft45
                        ? hitLeft45
                        : hitRight45;

                Destructable target = thit.collider.transform.root.GetComponent<Destructable>();
                if (target != null && target == _Target)
                {
                    if (ship.velocity.sqrMagnitude > 0 || (target.transform.position - _Ship.transform.position).magnitude < 1.5f)
                    {
                        _Thrust = -1;
                    }
                    return;
                }
            }

            if (hit || hitLeft45 || hitRight45)
            {
                if (ship.velocity.sqrMagnitude < 4 && ship.velocity.sqrMagnitude > 0.5)
                {
                    _Thrust = -1;
                }
                else
                {
                    if (hitLeft45)
                    {
                        _Torque = -1;
                        _Thrust = _Thrust * 0.6f;
                    }
                    else
                    {
                        _Torque = 1;
                        _Thrust = _Thrust * 0.6f;
                    }
                }
            }
        }

        Vector2 getRandomPointInCircle(Vector2 center, float radius)
        {
            Vector2 randomCirclePoint = UnityEngine.Random.insideUnitSphere * _PatrolAreaRadius;
            return new Vector2(
                randomCirclePoint.x + _PatrolAreaCenter.x,
                randomCirclePoint.y + _PatrolAreaCenter.y
            );
        }

        void PatrolAreaNewDestination()
        {
            _Destination = getRandomPointInCircle(_PatrolAreaCenter, _PatrolAreaRadius);
        }

        void PatrolAreaCalcCourse()
        {
            _Thrust = 0;
            _Torque = 0;

            if (_Target == null)
            {
                if (_Destination == Vector2.zero)
                {
                    PatrolAreaNewDestination();
                }
            }
            else
            {
                if (_Follow == AIFollow.Simple)
                {
                    _Destination = new Vector2(_Target.transform.position.x, _Target.transform.position.y);
                }
                else
                {
                    Rigidbody2D target = _Target.GetComponent<Rigidbody2D>();
                    Vector3 velocity = target.velocity;

                    Vector2 distToTarget = new Vector2(
                        _Ship.transform.position.x - _Target.transform.position.x,
                        _Ship.transform.position.y - _Target.transform.position.y
                    );

                    // very approximately and only for linear movement
                    // but still works to some extent
                    _Destination = Vector3.Lerp(_Target.transform.position, _Target.transform.position + velocity, Mathf.Sqrt(velocity.magnitude) * distToTarget.magnitude / (5 * Time.deltaTime));
                }
            }

            Vector2 distance = new Vector2(
                _Ship.transform.position.x - _Destination.x,
                _Ship.transform.position.y - _Destination.y
            );

            if (distance.sqrMagnitude < 2)
            {
                _Thrust = 0;
                _Torque = 0;
            }

            _Thrust = 1;

            if (distance.sqrMagnitude > 100)
            {
                _Thrust = _MaxThrust;
            }
            else
            {
                _Thrust = _MaxThrust * (1 - 1f / distance.sqrMagnitude);

                if (_Thrust < 0)
                {
                    _Thrust = 0;
                }
            }

            Vector2 localCoordinatesDest = _Ship.transform.InverseTransformPoint(_Destination);

            float angleToDestination = Vector3.SignedAngle(localCoordinatesDest, Vector3.up, Vector3.forward);

            float maxTorqueAngle = 10;

            angleToDestination = -Mathf.Clamp(angleToDestination, -maxTorqueAngle, maxTorqueAngle);

            _Torque = _MaxTorque * angleToDestination / maxTorqueAngle;
        }

        void PatrolPathCalcCourse()
        {
            _Thrust = 0;
            _Torque = 0;

            if (_Target == null)
            {
                _Destination = _PatrolPath[_CurrentPoint];
            }
            else
            {
                if (_Follow == AIFollow.Simple)
                {
                    _Destination = new Vector2(_Target.transform.position.x, _Target.transform.position.y);
                }
                else
                {
                    Rigidbody2D target = _Target.GetComponent<Rigidbody2D>();
                    Vector3 velocity = target.velocity;

                    Vector2 distToTarget = new Vector2(
                        _Ship.transform.position.x - _Target.transform.position.x,
                        _Ship.transform.position.y - _Target.transform.position.y
                    );

                    _Destination = Vector3.Lerp(_Target.transform.position, _Target.transform.position + velocity, distToTarget.magnitude / (velocity.magnitude * Time.deltaTime));
                }
            }

            Vector2 distance = new Vector2(
                    _Ship.transform.position.x - _Destination.x,
                    _Ship.transform.position.y - _Destination.y
                );

            if (_Target != null && distance.sqrMagnitude < 2)
            {
                _Thrust = 0;
                _Torque = 0;
            }

            if (_Target == null && distance.sqrMagnitude < 1)
            {
                _CurrentPoint += 1;
                if (_CurrentPoint >= _PatrolPath.Count)
                {
                    _CurrentPoint = 0;
                }
            }

            _Thrust = _MaxThrust;

            Vector2 localCoordinatesDest = _Ship.transform.InverseTransformPoint(_Destination);

            float angleToDestination = Vector3.SignedAngle(localCoordinatesDest, Vector3.up, Vector3.forward);

            float maxTorqueAngle = 10;

            angleToDestination = -Mathf.Clamp(angleToDestination, -maxTorqueAngle, maxTorqueAngle);

            _Torque = _MaxTorque * angleToDestination / maxTorqueAngle;
        }

        void ShipControls()
        {
            _Ship.ThrustControl = _Thrust;
            _Ship.TorqueControl = _Torque;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawCircle(new Vector3(_Destination.x, _Destination.y, 0), 1, 10, Color.green);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_Ship.transform.position, _Ship.transform.position + _Ship.transform.up.normalized * DetectObstacleDistance);

            if (_State == AIState.PatrolArea)
            {
                GizmosExtensions.DrawCircle(new Vector3(_PatrolAreaCenter.x, _PatrolAreaCenter.y, 0), _PatrolAreaRadius, 20, Color.yellow);
            }

            if (_State == AIState.PatrolPath)
            {
                foreach (var patrolPoint in _PatrolPath)
                {
                    GizmosExtensions.DrawCircle(new Vector3(patrolPoint.x, patrolPoint.y, 0), 1.1f, 10, Color.magenta);
                }
            }

            GizmosExtensions.DrawCircle(new Vector3(_Ship.transform.position.x, _Ship.transform.position.y, 0), _DetectionRange, 10, Color.red);
        }

#endif
    }
}
