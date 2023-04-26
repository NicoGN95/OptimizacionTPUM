using System.Collections.Generic;
using _Main.Scripts.Datas;
using _Main.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Entities
{
    public class VehicleController : MonoBehaviour
    {

        [SerializeField] private PlayerInput input;
        private VehicleModel m_vehicleModel;

        private InputActionMap m_vehicleInputActions;

        private void Start()
        {
            m_vehicleModel = GetComponent<VehicleModel>();
            // Get the input actions
            m_vehicleInputActions = input.currentActionMap;

            // Subscribe to the input actions using events
            
            m_vehicleInputActions["Movement"].performed += OnAcceleratePerformed;
            m_vehicleInputActions["Movement"].canceled += OnAccelerateCanceled;
            m_vehicleInputActions["EmergencyBrake"].performed += OnEmergencyBrakePerformed;
        }


        private void OnAcceleratePerformed(InputAction.CallbackContext context)
        {
            var l_inputValue = context.ReadValue<Vector2>();
            
            m_vehicleModel.ChangeDir(l_inputValue);
        }

        private void OnAccelerateCanceled(InputAction.CallbackContext context)
        {
            
            m_vehicleModel.ChangeDir(Vector2.zero);
        }

        private void OnEmergencyBrakePerformed(InputAction.CallbackContext context)
        {
            m_vehicleModel.EmergencyBrake();
        }
    }
}
