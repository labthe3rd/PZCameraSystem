/**
 *      Programmer:     labthe3rd
 *      Date:           11/18/2023
 *      Description:    Toggle to start the camera controller.
 */


using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace PartyZone
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CameraTrigger : UdonSharpBehaviour
    {
        [SerializeField] private GameObject CameraSystem;
        [SerializeField] private Camera TargetCamera;
        [SerializeField] private GameObject TargetCameraGameObject;

        [Header("Test Mode?")]
        [SerializeField] private bool testMode;

        [Header("Enable Debug Log?"), Tooltip("Enabling this will display logs of key points in the script")]
        [SerializeField] private bool debugMode;
        void Start()
        {
            TargetCamera.enabled = true;
            if (testMode)
            {
                CameraSystem.SetActive(true);
                TargetCameraGameObject.SetActive(true);
                TargetCamera.depth = 2;
            }
        }

        public override void Interact()
        {
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                if (!Networking.LocalPlayer.IsUserInVR())
                {
                    CameraSystem.SetActive(true);
                    TargetCamera.depth = 2;
                    TargetCameraGameObject.SetActive(true);

                }
                else
                {
                    DebugMessage("This mode can only be used by players on Desktop");
                }
            }
        }

        public void ExitCamera()
        {
            TargetCamera.depth = -2;
            TargetCameraGameObject.SetActive(false);
            CameraSystem.SetActive(false);
        }



        //Debug message display logic
        private void DebugMessage(string message)
        {
            if (debugMode)
            {
                Debug.Log(message);
            }
        }
    }
}

