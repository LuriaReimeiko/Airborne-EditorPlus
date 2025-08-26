using Airborne_EditorPlus;
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Airborne_EditorPlus
{
	public class Tools : MelonMod
	{
		readonly EditorControls editorControls = EditorPlus.editorControls;
		public void ChangeActiveState(GameObject obj)
		{
			obj.SetActive(!obj.activeSelf);
		}

		public void SelectObject()
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
		}

		public void PickObject()
		{
			RaycastHit hit;

			if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), editorControls.transform.forward, out hit, Mathf.Infinity, editorControls.selectIgnore))
			{

				if (Array.IndexOf(editorControls.lastSelectedArray, hit.transform.root.gameObject) != -1) return;

				editorControls.DeselectPortal();
				editorControls.Deselect();
				editorControls.selectedObject = false;

				// There seems to be no better way as this is how the game is made
				string name = hit.transform.root.gameObject.name;
				if (name.Contains("Platform"))
					editorControls.ToggleButton(1);
				else if (name.Contains("Spike"))
					editorControls.ToggleButton(2);
				else if (name.Contains("button"))
					editorControls.ToggleButton(3);
				else if (name.Contains("Turret"))
					editorControls.ToggleButton(4);
				else if (name.Contains("Laser"))
					editorControls.ToggleButton(6); // why is only laser
				else if (name.Contains("Launch Pad"))
					editorControls.ToggleButton(5); // and launch pad inverted
				else if (name.Contains("text"))
					editorControls.ToggleButton(7);
				else if (name.Contains("Teleporter"))
					editorControls.ToggleButton(8);
				else if (name.Contains("Checkpoint"))
					editorControls.ToggleButton(9);
			}
		}

		public void DeselectObject()
		{
			editorControls.Deselect();
			editorControls.DeselectPortal();
			editorControls.SetUI(false);
		}

		public void SetObjectColor(GameObject obj, Color color)
		{
			obj.GetComponent<MeshRenderer>().material.color = color;
			//Material coloredMat = new Material(new Shader());
			//coloredMat.color = color;
			//obj.GetComponent<MeshRenderer>().material = coloredMat;
		}

		public void Pan()
		{
			EditorControls.uType lastuType = EditorControls.uType.none;
			bool lastSelected = false;

			if (Input.GetMouseButtonDown(0))
			{
				lastuType = editorControls.UIType;
				editorControls.UIType = EditorControls.uType.none;
				editorControls.SetUI(true);

				lastSelected = editorControls.selectedObject;
			}
			if (Input.GetMouseButton(0))
			{
				Vector3 pos;
				pos.x = Mathf.Clamp(editorControls.mainCam.transform.position.x - (Input.GetAxisRaw("Mouse X") / 1.5f), -86.1f, 86.1f);
				pos.y = Mathf.Clamp(editorControls.mainCam.transform.position.y - (Input.GetAxisRaw("Mouse Y") / 1.5f), -78, 78);
				pos.z = -32;
				editorControls.mainCam.transform.position = pos;
			}
			if (Input.GetMouseButtonUp(0))
			{
				editorControls.UIType = lastuType;
				editorControls.SetUI(true);

				editorControls.selectedObject = lastSelected;
			}
		}
	}
}

// GPTGrid
public class TiledGrid : MonoBehaviour
{
	public Color lineColor = new Color(1f, 1f, 1f, 0.3f);

	private GameObject tiledGrid;
	private Material gridMat;

	EditorPlus editorPlus = new();

	public void Load()
	{
		// 1. Create a simple white 1x1 texture for the lines
		Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
		tex.SetPixel(0, 0, Color.white);
		tex.Apply();

		// 2. Create quad
		tiledGrid = GameObject.CreatePrimitive(PrimitiveType.Quad);
		tiledGrid.name = "tiledGrid";
		tiledGrid.transform.position = Vector3.zero;
		tiledGrid.transform.localScale = new Vector3(10, 10, 1); // world size of quad

		// 3. Create material
		gridMat = new Material(Shader.Find("Unlit/Texture"));
		gridMat.mainTexture = tex;
		gridMat.color = lineColor;

		tiledGrid.GetComponent<Renderer>().material = gridMat;

		// 4. Set tiling
		UpdateTiling();
	}

	void UpdateTiling()
	{
		// How many repeats per quad based on tile size
		float repeatsX = tiledGrid.transform.localScale.x / editorPlus.snapSize;
		float repeatsY = tiledGrid.transform.localScale.y / editorPlus.snapSize;
		gridMat.mainTextureScale = new Vector2(repeatsX, repeatsY);
	}
}
