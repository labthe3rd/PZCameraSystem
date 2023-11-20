/**
 *      Programmer:     labthe3rd
 *      Date:           11/18/2023
 *      Description:    Allows user to change between cinamachine cameras.
 */


using Cinemachine;
using UdonSharp;
using UnityEngine;


namespace PartyZone
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CameraController : UdonSharpBehaviour
    {
        [Header("Camera Settings")]
        public CameraTrigger targetController;
        public CinemachineVirtualCamera[] targetCameras;

        [Space]
        [Header("Enable Debug Log?"), Tooltip("Enabling this will display logs of key points in the script")]
        [SerializeField] private bool debugMode;

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                targetController.ExitCamera();
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (targetCameras.Length > 0)
                {
                    SetPriorityAll();
                    targetCameras[0].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (targetCameras.Length > 1)
                {
                    SetPriorityAll();
                    targetCameras[1].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                if (targetCameras.Length > 2)
                {
                    SetPriorityAll();
                    targetCameras[2].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                if (targetCameras.Length > 3)
                {
                    SetPriorityAll();
                    targetCameras[3].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                if (targetCameras.Length > 4)
                {
                    SetPriorityAll();
                    targetCameras[4].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                if (targetCameras.Length > 5)
                {
                    SetPriorityAll();
                    targetCameras[5].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                if (targetCameras.Length > 6)
                {
                    SetPriorityAll();
                    targetCameras[6].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                if (targetCameras.Length > 7)
                {
                    SetPriorityAll();
                    targetCameras[7].Priority = 20;

                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                if (targetCameras.Length > 8)
                {
                    SetPriorityAll();
                    targetCameras[8].Priority = 20;

                }
            }

        }

        private void SetPriorityAll()
        {
            if (targetCameras.Length > 0)
            {
                for (int i = 0; i < targetCameras.Length; i++)
                {
                    targetCameras[i].Priority = 0;
                }
            }
        }

        public void EnableCamera(int selectedCamera)
        {
            if(selectedCamera < targetCameras.Length)
            {
                SetPriorityAll();
                targetCameras[selectedCamera].Priority = 20;
            }
        }

        public void ExitCamera()
        {
            targetController.ExitCamera();
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

