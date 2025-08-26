using Airborne_EditorPlus;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using static Il2Cpp.EditorControls;

namespace Airborne_EditorPlus
{
	public class VanillaBehaviour
	{
		readonly EditorControls editorControls = EditorPlus.editorControls;

		readonly MainEditorComponent mainEditorComponent = EditorPlus.editorControls.mainEditorComponent; // was tired typing allat
		GameObject placeObject => EditorPlus.editorControls.placeObject;
		int selectedButton => EditorPlus.editorControls.selectedButton;
		GameObject lastSelected => EditorPlus.editorControls.lastSelected;

		readonly Tools tools = new Tools(); 


		public void OverrideUpdate()
		{
			if (mainEditorComponent.finale) return; // This shouldn't be possible but let's keep it just in case

			editorControls.objectCount.text = $"Objects: {mainEditorComponent.levelObjects.Length}/19000";

			if (mainEditorComponent.beat)
			{
				editorControls.levelVerified.text = "Verified";
			}
			else
			{
				editorControls.levelVerified.text = "Unverified";
			}

			if (!MainEditorComponent.editable) return;

			OverrideVanillaHotkeys();
			editorControls.mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 32));

			OverrideCollisions();

#region OBJECT PLACEMENT
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				if (Input.GetMouseButton(0) && editorControls.canObjectBePlaced && selectedButton > 0 && selectedButton != 10)
				{
					mainEditorComponent.beat = false;
					if (selectedButton == 1)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|false,255,255,255,false,255,255,255");
					else if (selectedButton == 7)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|Sample Text,10,false,255,255,255,false,255,255,255,false,false");
					else if (selectedButton == 3)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|false,false,false");
					else if (selectedButton == 4)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|false,7,5,0");
					else if (selectedButton == 5)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|0,200");
					else if (selectedButton == 9)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|true,1,false");
					else if (selectedButton == 8)
						if (editorControls.spawnPairObjects)
						{
							int index = mainEditorComponent.blackHoles.Count / 2;
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, $"false,false,0,0,0,0,0,false,0,false,1,false,false,false|false,{index},false,false");
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x + 4,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, $"false,false,0,0,0,0,0,false,0,false,1,false,false,false|true,{index},false,false");
						}
						else
						{
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, "false,false,0,0,0,0,0,false,0,false,1,false,false,false|false,0,false,false");
						}


					else mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, "false,0,0,0,0,0,0,false,0,false,1,false,false,false|");

				}
			}
			else
			{
				if (Input.GetMouseButtonDown(0) && editorControls.canObjectBePlaced && selectedButton > 0 && selectedButton != 10)
				{
					mainEditorComponent.beat = false;
					if (selectedButton == 1)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|false,255,255,255,255,false,255,255,255,255");
					else if (selectedButton == 7)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|Sample Text,10,false,255,255,255,255,false,255,255,255,255,false,false");
					else if (selectedButton == 3)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|false,false,false");
					else if (selectedButton == 4)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|false,7,5,0");
					else if (selectedButton == 5)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|0,200");
					else if (selectedButton == 9)
						mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
							placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
							placeObject.transform.localScale.x,
							placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|true,1,false");
					else if (selectedButton == 8)
						if (editorControls.spawnPairObjects)
						{
							int index = mainEditorComponent.blackHoles.Count / 2;
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, $"false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|false,{index},false,false");
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x + 4,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, $"false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|true,{index},false,false");
						}
						else
						{
							mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|false,0,false,false");
						}


					else mainEditorComponent.CreateObject(selectedButton, placeObject.transform.position.x,
								placeObject.transform.position.y, placeObject.transform.localEulerAngles.z,
								placeObject.transform.localScale.x,
								placeObject.transform.localScale.y, true, "false,false,true,true,true,true,0!0!0!0!0~,false,0,false,1,false,false,false|");
				}
			}

			#endregion

#region DELETION
			if (Input.GetMouseButtonDown(0) && selectedButton == 10)
			{
				RaycastHit hit;
				if (Physics.Raycast(editorControls.mousePos, editorControls.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, editorControls.deleteIgnore))
				{
					editorControls.Delete(new[] { hit.transform.root.gameObject });
				}
			}

			else if (selectedButton == 10)
			{
				RaycastHit hit;
				if (Physics.Raycast(editorControls.mousePos, editorControls.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, editorControls.deleteIgnore) && selectedButton == 10)
				{
					if (hit.transform.root.TryGetComponent(out TMP_Text tmp))
					{
						editorControls.ChangeChildrenMaterials(hit.transform.root.gameObject, editorControls.textErrorMat, true, false, false);
					}
					else editorControls.ChangeChildrenMaterials(hit.transform.root.gameObject, editorControls.errorMat, true, false, false);
					if (lastSelected != null && hit.transform.root.gameObject != lastSelected) lastSelected.GetComponent<ObjectComponent>().ResetMaterials();
					editorControls.lastSelected = hit.transform.root.gameObject;
					return;
				}
				else
				{
					if (lastSelected != null && lastSelected.GetComponent<ObjectComponent>() != null)
					{
						lastSelected.GetComponent<ObjectComponent>().ResetMaterials();
						editorControls.lastSelected = null;
					}
				}
			}
			#endregion

#region SELECTION
			if ((editorControls.pstate == PlayState.play || editorControls.pstate == PlayState.pause) && MainEditorComponent.editable)
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					editorControls.Stop();
				}
			}

			if (editorControls.pstate == PlayState.stop)
			{
				if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), editorControls.transform.forward, Mathf.Infinity, editorControls.env))
				{
					foreach (GameObject wall in editorControls.walls)
					{
						wall.GetComponent<Renderer>().material = editorControls.transparent;
					}
				}
				else
				{
					foreach (GameObject wall in editorControls.walls)
					{
						wall.GetComponent<Renderer>().material = editorControls.border;
					}
				}
			}

			if (!editorControls.cantContinue && editorControls.pstate == PlayState.stop)
			{
				if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.A) && editorControls.pstate == PlayState.stop && selectedButton != 10 && !editorControls.cantContinue)
				{
					editorControls.Deselect();
					editorControls.DeselectPortal();
					for (int i = 1; i < mainEditorComponent.levelObjects.Length; i++)
					{
						editorControls.Select(mainEditorComponent.levelObjects[i], true);
					}
				}
				if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.D) && editorControls.selectedObject && !editorControls.cantContinue) editorControls.Deselect();
				if (selectedButton == 0 && Input.GetMouseButtonDown(0))
				{
					RaycastHit hit;

					if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), editorControls.transform.forward, out hit, Mathf.Infinity, editorControls.selectIgnore))
					{

						if (Array.IndexOf(editorControls.lastSelectedArray, hit.transform.root.gameObject) == -1)
						{
							if (!Input.GetKey(KeyCode.LeftControl))
							{
								editorControls.Deselect();
								editorControls.DeselectPortal();
								editorControls.Select(hit.transform.root.gameObject, false);
							}
							else editorControls.Select(hit.transform.root.gameObject, true);
							return;
						}
						else
						{
							List<GameObject> t = editorControls.lastSelectedArray.ToList();
							t.Remove(hit.transform.root.gameObject);
							editorControls.Deselect();
							editorControls.DeselectPortal();
							for (int i = 0; i < t.Count; i++)
							{
								editorControls.Select(t[i], true);
							}

						}

					}
					else if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), editorControls.transform.forward, out hit, Mathf.Infinity) && hit.transform.root.gameObject.CompareTag("portal"))
					{
						editorControls.SelectPortal(hit.transform.root.gameObject);
					}
					else
					{
						editorControls.Deselect();
						editorControls.DeselectPortal();
					}
				}
			}
			#endregion

#region CAMERA CONTROLS
			if (!editorControls.cantContinue)
			{
				if (Input.GetKeyDown(KeyCode.G))
				{
					editorControls.UIType = uType.pos;
				}
				else if (Input.GetKeyDown(KeyCode.R))
				{
					editorControls.UIType = uType.rot;
				}
				else if (Input.GetKeyDown(KeyCode.S))
				{
					editorControls.UIType = uType.scale;
				}
				else if (Input.GetKeyDown(KeyCode.T))
				{
					editorControls.UIType = uType.none;
				}
			}


			if (editorControls.selectedObject)
			{


				if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl) && !editorControls.cantContinue)
				{
					editorControls.clipboard = new GameObject[editorControls.lastSelectedArray.Length];
					editorControls.clipboard = editorControls.lastSelectedArray;
				}
				if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl) && !editorControls.cantContinue)
				{
					editorControls.Duplicate(editorControls.clipboard);
				}
				editorControls.SetUI(true);
				if (Input.GetKeyDown(KeyCode.Delete))
				{
					editorControls.Delete(editorControls.lastSelectedArray);
				}
			}
			else editorControls.SetUI(false);
			#endregion
		}

#region Handle Keybinds
		private void OverrideVanillaHotkeys()
		{
			if (editorControls.pstate == EditorControls.PlayState.stop) // i never should've trusted cantContinue
			{
					if (Input.GetKeyDown(KeyCode.U) && editorControls.upload.interactable) tools.ChangeActiveState(editorControls.uploadPanel);
				else if (Input.GetKeyDown(KeyCode.M)) EnableSettings();
				else if (Input.GetKeyDown(KeyCode.P)) Play();

				HandleCamera();
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

		static private Vector2 inputVector = Vector2.zero;
		static private Vector2 velocity = Vector2.zero;

		#region Camera
		Camera mainCam = EditorPlus.editorControls.mainCam;
		private void HandleCamera()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) ResetCamera();

			HandleZoom();
			MoveCameraInput();

			LimitCamera();
		}

		private void MoveCameraInput()
		{
#region Arrow Keys
			float speed = 35;
			if (Input.GetKey(KeyCode.LeftControl)) speed = 80;
			if (Input.GetKey(KeyCode.LeftShift)) speed = 15;

			inputVector.y = Convert.ToSingle(Input.GetKey(KeyCode.UpArrow)) - Convert.ToSingle(Input.GetKey(KeyCode.DownArrow));
			inputVector.x = Convert.ToSingle(Input.GetKey(KeyCode.RightArrow)) - Convert.ToSingle(Input.GetKey(KeyCode.LeftArrow));
			inputVector.Normalize();

			velocity = Vector2.Lerp(velocity, speed * inputVector, 10 * Time.deltaTime);
			mainCam.transform.Translate(velocity * Time.deltaTime);
#endregion
#region Middle Mouse
			if (Input.GetMouseButton(2))
			{
				Vector2 pos;
				pos.x = Input.GetAxisRaw("Mouse X") * mainCam.fieldOfView / 64;
				pos.y = Input.GetAxisRaw("Mouse Y") * mainCam.fieldOfView / 64;
				mainCam.transform.Translate(-pos);
			}
#endregion
		}

		private void HandleZoom()
		{
			if (!editorControls.cantScroll)
			{
				Vector2 originalPos, newPos;
				originalPos = mainCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

				mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView - (Input.GetAxisRaw("Mouse ScrollWheel")) * 15, 15, 120);

				newPos = mainCam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				editorControls.mainCam.transform.Translate(newPos - originalPos);
			}

			if (Input.GetKeyDown(KeyCode.Equals)) mainCam.fieldOfView += Mathf.Clamp(mainCam.fieldOfView - 7.5f, 15, 120);
			else if (Input.GetKeyDown(KeyCode.Minus)) mainCam.fieldOfView -= Mathf.Clamp(mainCam.fieldOfView + 7.5f, 15, 120);
		}

		private void ResetCamera()
		{
			mainCam.transform.position.Set(0, 0, -32);
			mainCam.fieldOfView = 36;
		}

		private void LimitCamera()
		{
			mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, 15, 120);
			// why can't i assign a Vector3 in this, stupid Unity
			mainCam.transform.position.Set(Mathf.Clamp(mainCam.transform.position.x, -86.1f, 86.1f), Mathf.Clamp(mainCam.transform.position.y, -78, 78), -32);
		}
		#endregion Camera

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
		#endregion

#region Handle Collisions
		private void OverrideCollisions()
		{
			if (placeObject.transform.root.TryGetComponent(out TMP_Text tmep))
			{
				editorControls.ChangeChildrenMaterials(placeObject.transform.root.gameObject, editorControls.textSelectedMat, true, false, false);
			}
			if (placeObject.transform.root.TryGetComponent(out ColorComponent cc))
			{
				cc.Disable();
			}
			if (editorControls.selectedButton > 0 && !editorControls.pMenu.isPaused && editorControls.pstate == EditorControls.PlayState.stop)

			{

				editorControls.snapMousePos = new Vector3(Mathf.RoundToInt(editorControls.mousePos.x), Mathf.RoundToInt(editorControls.mousePos.y), editorControls.mousePos.z);
				RaycastHit rayHit;
				if (Input.GetKeyDown(KeyCode.R))
				{
					placeObject.transform.eulerAngles -= new Vector3(0, 0, 45);
				}
				if (Input.GetKeyDown(KeyCode.Q))
				{
					placeObject.transform.eulerAngles += new Vector3(0, 0, 45);
				}
				if (!Input.GetKey(KeyCode.LeftShift))
				{
					placeObject.transform.position = editorControls.snapMousePos;
				}
				else if (Input.GetKey(KeyCode.LeftShift))
				{
					placeObject.transform.position = editorControls.mousePos;
				}
				if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), Vector3.forward, out rayHit, Mathf.Infinity, editorControls.placeIgnore) || editorControls.UIStop)
				{
					editorControls.canObjectBePlaced = false;
					if (placeObject.transform.root.TryGetComponent(out TMP_Text tmp))
					{
						editorControls.ChangeChildrenMaterials(placeObject.transform.root.gameObject, editorControls.textErrorMat, true, false, false);
					}
					else editorControls.ChangeChildrenMaterials(placeObject.transform.root.gameObject, editorControls.errorMat, true, false, false);
					editorControls.mousePos.Set(editorControls.mousePos.x, editorControls.mousePos.y, -2f);
				}
				else
				{
					editorControls.canObjectBePlaced = true;
					if (placeObject.transform.root.TryGetComponent(out TMP_Text tmp))
					{
						editorControls.ChangeChildrenMaterials(placeObject.transform.root.gameObject, editorControls.textSelectedMat, true, false, false);
					}
					else editorControls.ChangeChildrenMaterials(placeObject.transform.root.gameObject, editorControls.selectedMat, true, false, false);

				}
			}
		}
#endregion
	}
}
