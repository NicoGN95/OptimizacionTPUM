using System;
using System.Collections.Generic;
using _Main.Scripts.CustomUpdate;
using _Main.Scripts.Datas;
using _Main.Scripts.Managers;
using UnityEngine;

namespace _Main.Scripts.Entities
{
    public class VehicleModel : MonoBehaviour, IUpdateObject
    {
        [SerializeField] private VehicleData data;

        private Rigidbody m_rb;

        private float m_currSpeed;
        public float CurrSpeed => m_currSpeed;
        private Vector2 m_currDir;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SubscribeUpdateManager();
        }

        public void MyUpdate()
        {
            Move(m_currDir);
        }
        

        private void Move(Vector2 dir)
        {
            // Accelerate the vehicle
            
            var l_accForce = transform.forward;
            LookDir(dir);
            
            if (dir.y > 0.2)
            {
                l_accForce *= dir.y * data.Acceleration;
            }
            else if (dir.y < 0.2)
            {
                l_accForce *= dir.y * data.BreakForce;
            }
            
            m_rb.AddForce(l_accForce, ForceMode.Force);
            m_currSpeed = m_rb.velocity.magnitude;
        }

        private void LookDir(Vector2 dir)
        {
            var l_transform = transform;
            transform.forward = Vector3.Lerp(l_transform.forward,l_transform.right * dir.x, Time.deltaTime * data.TurnForce);
        }
        public void ChangeDir(Vector2 newDir)
        {
            m_currDir = newDir;
        }

        public void EmergencyBrake()
        {
            // Brake the vehicle hard
            m_rb.AddForce(transform.forward * -data.EmergencyBreakForce, ForceMode.Force);
        }


        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListenerGamePlay(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListenerGamePlay(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
