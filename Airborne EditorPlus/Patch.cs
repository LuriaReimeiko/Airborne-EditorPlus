using Airborne_EditorPlus;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace Airborne_EditorPlus
{
	public class VanillaBehaviour
	{
		EditorControls editorControls = EditorPlus.editorControls;
		Tools tools = new Tools();
		public void OverrideVanillaHotkeys()
		{
			if (editorControls.pstate == EditorControls.PlayState.stop && !editorControls.cantContinue) // i still dont know what cantContinue is but i'll trust
			{
				if (Input.GetKeyDown(KeyCode.U) && editorControls.upload.interactable) tools.ChangeActiveState(editorControls.uploadPanel);
				else if (Input.GetKeyDown(KeyCode.M)) EnableSettings();
				else if (Input.GetKeyDown(KeyCode.P)) Play();

				MoveCameraInput();
				HandleObjectSelection();
			}
			else if (editorControls.pstate != EditorControls.PlayState.stop)
			{
				if (Input.GetKeyDown(KeyCode.P))
				{
					if (Input.GetKey(KeyCode.LeftShift))
					{
						Play();
					}
					else
					{
						editorControls.playButtons[1].isOn = !editorControls.playButtons[1].isOn;
						editorControls.Pause();
					}
				}
			}
		}

		private bool DidSettingsInit = false;
		private void EnableSettings()
		{
			if (!DidSettingsInit)
			{
				editorControls.levelSettings.Init();
				DidSettingsInit = true;
			}
			editorControls.settingsCanvas.SetActive(!editorControls.settingsCanvas.activeSelf);
		}

		private void Play()
		{
			editorControls.playButtons[0].isOn = !editorControls.playButtons[0].isOn;
			editorControls.Play();
			editorControls.canvas.SetActive(editorControls.playButtons[0].isOn);
		}

		private void MoveCameraInput()
		{
			float fov = editorControls.mainCam.fieldOfView;
			if (Input.GetKeyDown(KeyCode.Equals)) editorControls.mainCam.fieldOfView = Mathf.Min(Mathf.Max(fov - 7.5f, 15), 120);
			else if (Input.GetKeyDown(KeyCode.Minus)) editorControls.mainCam.fieldOfView = Mathf.Min(Mathf.Max(fov + 7.5f, 15), 120);




			//float horizontalInput = Input.GetAxis("Horizontal");
			//float verticalInput = Input.GetAxis("Vertical");

			//Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
			//Vector3 moveVector = moveDirection * 20 * Time.deltaTime;

			//editorControls.mainCam.transform.Translate(moveVector);

			//// Decelerate the camera movement to make it stop faster
			//if (moveVector.magnitude > 0.01f) moveVector -= moveVector * 20 * Time.deltaTime;
			//else moveVector = Vector3.zero;

			//Vector3 clampedPosition = editorControls.mainCam.transform.position;
			//clampedPosition.x = Mathf.Clamp(clampedPosition.x, -86.1f, 86.1f);
			//clampedPosition.y = Mathf.Clamp(clampedPosition.y, -78, 78);
			//clampedPosition.z = -32;
			//editorControls.mainCam.transform.position = clampedPosition;



			// TODO rework
			Vector2 inputVector, velocity = new Vector2(0, 0);
			inputVector.x = Convert.ToSingle(Input.GetKey(KeyCode.DownArrow)) - Convert.ToSingle(Input.GetKey(KeyCode.UpArrow));
			inputVector.y = Convert.ToSingle(Input.GetKey(KeyCode.RightArrow)) - Convert.ToSingle(Input.GetKey(KeyCode.LeftArrow));
			inputVector.Normalize();

			Vector2.Lerp(velocity, inputVector, 100 * Time.deltaTime);
			editorControls.mainCam.transform.Translate(100 * velocity);
		}

		private void HandleObjectSelection()
		{
			for (KeyCode i = KeyCode.Alpha0; i <= KeyCode.Alpha9; i++)
			{
				if (Input.GetKeyDown(i))
				{
					editorControls.ToggleButton((int)i - (int)KeyCode.Alpha0);
				}
			}
		}
	}
}
