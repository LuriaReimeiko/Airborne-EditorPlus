using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace Airborne_EditorPlus
{
    public class Tools : MelonMod
    {
        EditorControls editorControls = EditorPlus.editorControls;
        public void ChangeActiveState(GameObject obj)
        {
            obj.SetActive(!obj.activeSelf);
        }

        public void SelectObject()
        {
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(editorControls.mousePos.x, editorControls.mousePos.y, -27), editorControls.transform.forward, out hit, Mathf.Infinity, editorControls.selectIgnore))
            {
                if (Array.IndexOf(editorControls.lastSelectedArray, hit.transform.root.gameObject) != -1) return;

                editorControls.DeselectPortal();
                editorControls.UIType = EditorControls.uType.none;
                editorControls.SetUI(true);

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    editorControls.Select(hit.transform.root.gameObject, true);
                }
                else
                {
                    editorControls.Deselect();
                    editorControls.Select(hit.transform.root.gameObject, false);
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
                pos.x = Mathf.Clamp(editorControls.mainCam.transform.position.x - (Input.GetAxisRaw("Mouse X") / (1.5f * (editorControls.mainCam.fieldOfView / 36))), -86.1f, 86.1f);
                pos.y = Mathf.Clamp(editorControls.mainCam.transform.position.y - (Input.GetAxisRaw("Mouse Y") / (1.5f * (editorControls.mainCam.fieldOfView / 36))), -78, 78);
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

        public void HandleZoom()
        {
            if (!editorControls.cantScroll && !Input.GetKey(KeyCode.LeftControl))
            {
                Vector3 pos;
                pos.x = Mathf.Clamp(editorControls.mainCam.transform.position.x - (Input.GetAxisRaw("Mouse X") * Input.GetAxisRaw("Mouse ScrollWheel")), -86.1f, 86.1f);
                pos.y = Mathf.Clamp(editorControls.mainCam.transform.position.y - (Input.GetAxisRaw("Mouse Y") * Input.GetAxisRaw("Mouse ScrollWheel")), -78, 78);
                pos.z = -32;

                editorControls.mainCam.transform.position = pos;

            }
        }
    }
}
