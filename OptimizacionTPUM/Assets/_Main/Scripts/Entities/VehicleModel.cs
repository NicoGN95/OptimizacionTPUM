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
        //All datas that this vehicle can turn into
        [SerializeField] private VehicleData currentData;

        private Rigidbody m_rb;

        private float m_currSpeed;
        public float CurrSpeed => m_currSpeed;
        private bool m_handBrake;
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
            var l_turnForce = transform.right * (dir.x * currentData.TurnForce);
            var l_accForce = transform.forward;
            LookDir(dir);
            if (dir.y > 0.2)
            {
                l_accForce *= dir.y * currentData.Acceleration;
            }
            else if (dir.y < 0.2)
            {
                l_accForce *= dir.y * currentData.BreakForce;
            }
            
            var force = l_turnForce + l_accForce;
            m_rb.AddForce(force, ForceMode.Force);
            m_currSpeed = m_rb.velocity.magnitude;
        }

        private void LookDir(Vector2 dir)
        {
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(dir.x, 0 , dir.y), Time.deltaTime * 10);
        }
        public void ChangeDir(Vector2 newDir)
        {
            m_currDir = newDir;
        }

        public void EmergencyBrake()
        {
            // Brake the vehicle hard
            m_rb.AddForce(transform.forward * -currentData.EmergencyBreakForce, ForceMode.Acceleration);
            m_handBrake = true;
        }


        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListenerGamePlay(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListenerGamePlay(this);
        }

        
    }
}
