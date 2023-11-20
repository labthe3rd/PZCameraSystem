# PZCameraSystem

Camera Control System for VRChat. Allows users that are in desktop to switch between multiple cameras for recording and DJing.

## Enjoying the prefab?

<form action="https://www.paypal.com/donate" method="post" target="_top">
<input type="hidden" name="business" value="QXEYT9DHDXAUC" />
<input type="hidden" name="no_recurring" value="0" />
<input type="hidden" name="item_name" value="Help inspire me to continue creating new VRChat Prefabs and other software!" />
<input type="hidden" name="currency_code" value="USD" />
<input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif" border="0" name="submit" title="PayPal - The safer, easier way to pay online!" alt="Donate with PayPal button" />
<img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1" />
</form>

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
