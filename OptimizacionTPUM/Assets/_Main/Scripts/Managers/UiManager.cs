using System;
using _Main.Scripts.CustomUpdate;
using _Main.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Main.Scripts.Managers
{
    public class UiManager : MonoBehaviour, IUpdateObject
    {
        [SerializeField] private Text speedText;
        [SerializeField] private VehicleModel model;
        [SerializeField] private GameObject needel;
        public static UiManager Instance;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;

        }

        private void Start()
        {
            SubscribeUpdateManager();
        }

        public void MyUpdate()
        {
            UpdateSpeed(model.CurrSpeed);
        }

        private void UpdateSpeed(float speed)
        {
            var l_value = speed * 3.6f;
            speedText.text = "Speed: " + Mathf.Round(l_value)+ "km/h";

            var l_angle = Quaternion.Euler(0, 0, -speed * 3.6f);
            needel.transform.rotation = l_angle;
        }

        public void SubscribeUpdateManager()
        {
            UpdateManager.Instance.AddListenerUI(this);
        }

        public void UnSubscribeUpdateManager()
        {
            UpdateManager.Instance.RemoveListenerUI(this);
        }

        
    }
}