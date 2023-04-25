using System;
using System.Collections.Generic;
using _Main.Scripts.Datas;
using _Main.Scripts.Managers;
using UnityEngine;

namespace _Main.Scripts.Entities
{
    public class VehicleModel : MonoBehaviour
    {
        //All datas that this vehicle can turn into
        [SerializeField] private List<VehicleData> allVehicleData = new List<VehicleData>();

        private VehicleData _currentData;
        private Rigidbody _rb;

        private float _currSpeed;
        public float CurrSpeed => _currSpeed;
        private bool _handBrake;
        private Vector2 _currDir;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _currentData = allVehicleData[0];
        }

        private void Update()
        {
            
            
            Move(_currDir);
        }

        private void Move(Vector2 dir)
        {
            // Accelerate the vehicle
            var force = new Vector3(dir.x * _currentData.TurnForce, 0, 0);
            LookDir(dir);
            if (dir.y > 0.2)
            {
                force.z = dir.y * _currentData.Acceleration;
            }
            else if (dir.y < 0.2)
            {
                force.z = dir.y * _currentData.BreakForce;
            }
            
            _rb.AddForce(force, ForceMode.Acceleration);
            _currSpeed = _rb.velocity.magnitude;
        }

        private void LookDir(Vector2 dir)
        {
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(dir.x, 0 , dir.y), Time.deltaTime * 10);
        }
        public void ChangeDir(Vector2 newDir)
        {
            _currDir = newDir;
        }

        public void EmergencyBrake()
        {
            // Brake the vehicle hard
            _rb.AddForce(transform.forward * -_currentData.EmergencyBreakForce, ForceMode.Acceleration);
            _handBrake = true;
        }

        
    }
}
