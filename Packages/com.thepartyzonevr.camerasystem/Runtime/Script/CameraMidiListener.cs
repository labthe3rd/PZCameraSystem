/*  Programmer:     labthe3rd
 *  Date:           11/19/2023
 *  Description:    Midi listener will store config info and send it 
 *  to the camera system when it becomes visible 
 */
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace PartyZone
{
    //No syncing neccessary for this script.
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CameraMidiListener : UdonSharpBehaviour
    {
        [Header("Main Settings")]
        public CameraController cameraController;
        [Space]
        [Header("UI Components")]
        public Text selectedCameraUI;
        public Button bindButton;
        public Button exitButton;
        public Text debugText;
        [Space]
        [Header("Camera Control UI")]
        public Text[] camMidiText;
        public Text exitMidiText;
        [Space]
        [Header("Display Log Messages")]
        [SerializeField] private bool debugMode = false;


        //UI Controls
        private int selectedCamera = 0;

        private bool bindMidiEnable; //Turn this bit on to listen for midi signal
        private bool bindExitEnable; //Turn on this bit to bind midi to exit.

        //Config information
        private int[] storedChannel; //Stored channel info for each camera
        private int[] storedNumber; //Stored number/note info for each camera
        private bool[] storedEnable; //Verify binding was actually set

        //Exit config info
        private int exitChannel;
        private int exitNumber;
        private bool exitEnable;

        private Color defaultBindButtonColor;
        private Color defaultExitButtonColor;
        private string defaultBindButtonString;
        private string defaultExitButtonString;
        private Text bindButtonText;
        private Text exitButtonText;

        //On start get the length that the user defined for their number of cameras.
        void Start()
        {
            //Set stored info array lengths to the same number of cameras
            storedChannel = new int[cameraController.targetCameras.Length];
            storedNumber = new int[cameraController.targetCameras.Length];
            storedEnable = new bool[cameraController.targetCameras.Length];

            //Get button defaults
            bindButtonText = bindButton.GetComponentInChildren<Text>();
            defaultBindButtonString = bindButtonText.text;
            defaultBindButtonColor = bindButton.image.color;

            //Get Exit Bind Button Defaults
            exitButtonText = exitButton.GetComponentInChildren<Text>();
            defaultExitButtonString = exitButtonText.text;
            defaultExitButtonColor = exitButton.image.color;
        }

        //Listen for the midi on event
        public override void MidiNoteOn(int channel, int number, int velocity)
        {
            DebugLog("Midi On - CH:" + channel + " NUM:" + number + " VEL:" + velocity);
            if (debugText != null && Utilities.IsValid(debugText) && !bindMidiEnable && !bindExitEnable)
            {
                debugText.text = "MIDI ON - CH:" + channel + " NUM:" + number + " VEL:" + velocity;
            }
            if (!cameraController.gameObject.activeSelf)
            {
                //Bind enabled
                if (bindMidiEnable && !bindExitEnable)
                {
                    //Verify channel and number are open to bind
                    for (int i = 0; i < cameraController.targetCameras.Length; i++)
                    {
                        if (storedEnable[i])
                        {
                            if (storedChannel[i] == channel && storedNumber[i] == number)
                            {
                                debugText.text = "CH:" + channel + " NUM:" + number + " is already binded to camera " + (i+1);
                                BindToggle();
                                return;
                            }
                        }
                    }

                    if (exitEnable)
                    {
                        if (exitChannel == channel && exitNumber == number)
                        {
                            debugText.text = "CH:" + channel + " NUM:" + number + " is already binded to exit command";
                            BindToggle();
                            return;
                        }
                    }


                    storedChannel[selectedCamera] = channel;
                    storedNumber[selectedCamera] = number;
                    storedEnable[selectedCamera] = true;
                    debugText.text = "Camera " + (selectedCamera + 1).ToString() + " CH:" + channel.ToString() + " NUM:" + number.ToString();
                    UpdateCamText(selectedCamera, channel, number);
                    BindToggle();
                }
                else if (!bindMidiEnable && bindExitEnable)
                {

                    //Verify channel and number are open to bind
                    for (int i = 0; i < cameraController.targetCameras.Length; i++)
                    {
                        if (storedEnable[i])
                        {
                            if (storedChannel[i] == channel && storedNumber[i] == number)
                            {
                                debugText.text = "CH:" + channel + " NUM:" + number + " is already binded to camera " + (i+1);
                                ExitToggle();
                                return;
                            }
                        }
                    }

                    if (exitEnable)
                    {
                        if (exitChannel == channel && exitNumber == number)
                        {
                            debugText.text = "CH:" + channel + " NUM:" + number + " is already binded to exit command";
                            ExitToggle();
                            return;
                        }
                    }

                    exitChannel = channel;
                    exitNumber = number;
                    exitEnable = true;
                    debugText.text = "Exit CMD Set To " + " CH:" + channel.ToString() + " NUM:" + number.ToString();
                    UpdateExitText(channel, number);
                    ExitToggle();
                }
            }
            else
            {
                //If bind buttons are left on, force them off.
                if (bindExitEnable)
                {
                    ExitToggle();
                }
                if (bindMidiEnable)
                {
                    BindToggle();
                }

                if (exitEnable)
                {

                    DebugLog(exitChannel + "==" + channel + exitNumber + "==" + number);
                    if (exitChannel == channel && exitNumber == number)
                    {
                        cameraController.ExitCamera();
                        return;
                    }
                }
                else
                {
                    DebugLog("No Error Binding For Midi");
                }

                //Check if midi command matches binding
                for (int i = 0; i < cameraController.targetCameras.Length; i++)
                {
                    if (storedEnable[i])
                    {
                        if (storedChannel[i] == channel && storedNumber[i] == number)
                        {
                            cameraController.EnableCamera(i);
                        }
                        else
                        {
                            DebugLog("Midi signal does not match at index " + i);
                        }
                    }
                    else
                    {
                        DebugLog("No binding at index " + i);
                    }
                }
            }

        }

        //Increment selected camera
        public void NextCamera()
        {
            if (selectedCamera < cameraController.targetCameras.GetUpperBound(0))
            {
                selectedCamera++;
            }
            else
            {
                selectedCamera = 0;
            }
            UpdateSelectedCamera();
            DisplayCurrentSettings(selectedCamera);
        }

        //Decrement selected camera
        public void PreviousCamera()
        {
            if (selectedCamera > 0)
            {
                selectedCamera--;
            }
            else
            {
                selectedCamera = cameraController.targetCameras.GetUpperBound(0);
            }
            UpdateSelectedCamera();
            DisplayCurrentSettings(selectedCamera);
        }

        //Update selected Camera text
        public void UpdateSelectedCamera()
        {
            //Offset select by 1 to make UI more human readable.
            selectedCameraUI.text = (selectedCamera + 1).ToString();
        }

        //Display current midi bind in console
        public void DisplayCurrentSettings(int camera)
        {
            string cameraString;
            string exitString;
            if (storedEnable[camera])
            {
                cameraString = "Camera " + (camera+1) + " CH:" + storedChannel[camera] + " NUM:" + storedNumber[camera];
            }
            else
            {
                cameraString = "Camera " + (camera+1) + " has no binding set.";
            }

            if (exitEnable)
            {
                exitString = "Exit CMD CH:" + exitChannel + " NUM:" + exitNumber;
            }
            else
            {
                exitString = "Exit CMD has no binding set.";
            }
            debugText.text = cameraString + "\n" + exitString;
        }

        public void BindToggle()
        {
            bindMidiEnable = !bindMidiEnable;

            if (bindMidiEnable)
            {
                bindButton.image.color = Color.green;
                bindButtonText.text = "Listening...";
            }
            else
            {
                bindButton.image.color = defaultBindButtonColor;
                bindButtonText.text = defaultBindButtonString;
            }
        }

        public void ExitToggle()
        {
            bindExitEnable = !bindExitEnable;

            if (bindExitEnable)
            {
                exitButton.image.color = Color.green;
                exitButtonText.text = "Listening...";
            }
            else
            {
                exitButton.image.color = defaultExitButtonColor;
                exitButtonText.text = defaultExitButtonString;
            }
        }

        private void UpdateCamText(int camera, int channel, int number)
        {
            camMidiText[camera].text = "CH" + channel + " NM" + number;
        }

        private void UpdateExitText(int channel, int number)
        {
            exitMidiText.text = "CH" + channel + " NM" + number;
        }

        private void DebugLog(string message)
        {
            if (debugMode)
            {
                Debug.Log(gameObject.name + " Log Message: " + message);
            }
        }
        private void DebugError(string message)
        {
            if (debugMode)
            {
                Debug.LogError(gameObject.name + " Log Message: " + message);
            }
        }
    }
}

