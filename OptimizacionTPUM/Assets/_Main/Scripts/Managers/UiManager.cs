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
            var value = Mathf.Clamp(speed * 3.6f, 0, 150);
            speedText.text = "Speed: " + Mathf.Round(value)+ "km/h";

            var angle = Quaternion.Euler(0, 0, -speed * 3.6f);
            needel.transform.rotation = angle;
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