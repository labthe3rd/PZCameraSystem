# PZCameraSystem

Camera Control System for VRChat. Allows users that are in desktop to switch between multiple cameras for recording and DJing.

## Enjoying the prefab?

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?business=QXEYT9DHDXAUC&no_recurring=0&item_name=Help+inspire+me+to+continue+creating+new+VRChat+Prefabs+and+other+software%21&currency_code=USD)

## ONLY FOR DESKTOP PLAYERS!

### This prefab uses cinnemachine. If you'd like to edit the camera motions, please look up cinnemachine tutorials.

### Camera Controls

- Use Numpad 1 through Numpad 9 to switch between virtual cameras.
- Push x or Numpad 0 to exit the camera system.

### Editing Cameras

- Move camera angles by selecting CM vcam1 - 9 child components.
- CM vcam1 is an example of how to use the trolly system.
- You can reduce the number of cameras by editing the array length in Camera System [Keep Hidden].
- You cannot add more cameras without editing the script and adding more inputs. If you want to add more edit the CameraController.cs file.

### Instructions

1.  Import the project using VRC Creator Companion or use the unity package.
2.  Drag the "PZ Camera System" prefab into your scene. It is located under Packages -> Party Zone Camera System -> Runtime.
3.  Run the scene and when you click the trigger it will override the player camera.

### Questions?

- Join The Party Zone discord server and send me a message! https://discord.gg/MYx7cEyW5C

### TODO

- Finish ReadME
- Add feature that will allow midi inputs to control cameras
- Add feature that will allow midi inputs to be mapped to specific camerras
