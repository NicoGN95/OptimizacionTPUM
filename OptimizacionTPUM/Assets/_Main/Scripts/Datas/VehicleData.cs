using UnityEngine;

namespace _Main.Scripts.Datas
{
    [CreateAssetMenu(fileName = "NewVehicleData", menuName = "_main/VehicleData")]
    public class VehicleData : ScriptableObject
    {
        [field : SerializeField] public float MaxSpeed{get; private set; }
        [field : SerializeField] public float Acceleration{get; private set; }
        [field : SerializeField] public float BreakForce{get; private set; }
        [field : SerializeField] public float EmergencyBreakForce{get; private set; }
        [field : SerializeField] public float TurnForce{get; private set; }
        [field : SerializeField] public float DriftForce{get; private set; }
    }
}
