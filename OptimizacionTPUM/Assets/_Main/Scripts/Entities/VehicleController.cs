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
        private VehicleModel _vehicleModel;

        private InputActionMap _vehicleInputActions;

        private void Start()
        {
            _vehicleModel = GetComponent<VehicleModel>();
            // Get the input actions
            _vehicleInputActions = input.currentActionMap;

            // Subscribe to the input actions using events
            
            _vehicleInputActions["Movement"].performed += OnAcceleratePerformed;
            _vehicleInputActions["EmergencyBrake"].performed += OnEmergencyBrakePerformed;
        }


        private void OnAcceleratePerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            //_vehicleModel.Movement(inputValue);
            _vehicleModel.ChangeDir(inputValue);
        }



        private void OnEmergencyBrakePerformed(InputAction.CallbackContext context)
        {
            _vehicleModel.EmergencyBrake();
        }
    }
}
