using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Airborne_EditorPlus.EditorPlus), "Airborne Editor+", "0.0.1", "Reimeiko", null)]
[assembly: MelonGame("bl4ckdev x XDev", "Airborne")]

namespace Airborne_EditorPlus
{
	public class EditorPlus : MelonMod
	{
		static public bool enabled;
		public float snapSize = 1f;

		static public GameObject Properties;
		static public GameObject UploadPanel;
		static public GameObject BuildUI;
		static public EditorControls editorControls;

#region Initialize
#region Enable mod
		public override void OnInitializeMelon()
		{
			enabled = false;
			AssetBundle.LoadFromFileAsync("Grid.shader");
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			editorControls = GameObject.FindObjectOfType<EditorControls>();

			if (sceneName == "ActualEditor" && editorControls.pstate == EditorControls.PlayState.stop)
			{
				if (!enabled) LoggerInstance.Msg("Editor+ is now Enabled.");
				enabled = true;
				GetStuff();
				OnLoad();
			}
			else
			{
				if (enabled) LoggerInstance.Msg("Editor+ is now Disabled.");
				enabled = false;
			}
		}
#endregion

		private void GetStuff()
		{
			var all = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach (var obj in all)
			{
				if (obj.name == "Build Mode UI") BuildUI = obj;
				else if (obj.name == "Level Upload") UploadPanel = obj;
				else if (obj.name == "Properties") Properties = obj;
				//else if (obj.name.Contains("Wall")) SetObjectColor(obj, Color.white); // Find how to make work
				else if (obj.name == "Platform") obj.transform.localScale = new Vector3(0,0,0);
			}

			// I dunno how to do this in the other loop sorry
			foreach (var cam in GameObject.FindObjectsOfType<Camera>())
			{
				cam.backgroundColor = new Color(0.4f,0.4f,0.6f);
				cam.clearFlags = CameraClearFlags.SolidColor;
			}
		}
		#endregion

		#region Mod
		private readonly Tools tool = new Tools();
		
		private void OnLoad()
		{
			//TiledGrid tiledGrid = new();
			//tiledGrid.Load();
		}

		public override void OnUpdate()
		{
			if (!enabled) return;
			VanillaBehaviour Vanilla = new();

			// Do modded stuff before vanilla stuff becuz i made it that way
			if (editorControls.pstate == EditorControls.PlayState.stop)
			{
				ModHotkeys();
			}
			
			Vanilla.OverrideUpdate();
		}

		private void ModHotkeys()
		{
			#region All

			if (Input.GetKey(KeyCode.Space))
				tool.Pan();

			if (Input.GetKeyDown(KeyCode.F1) && !editorControls.cantContinue)
				tool.ChangeActiveState(BuildUI);

			else if (Input.GetKeyDown(KeyCode.H) && editorControls.selectedObject)
				tool.ChangeActiveState(Properties); // Temp Key

			else if (Input.GetMouseButtonDown(1) && !editorControls.cantContinue)
				tool.SelectObject();

			#endregion

			#region LShift

			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKeyDown(KeyCode.U))
					tool.ChangeActiveState(UploadPanel);

				else if (Input.GetKeyDown(KeyCode.Backspace) && (editorControls.selectedObject))
					tool.ChangeActiveState(Properties);
			}

			#endregion

			#region LAlt

			if (Input.GetKey(KeyCode.LeftAlt))
			{
				if (Input.GetMouseButtonDown(0))
					tool.PickObject();
			}

			#endregion

			#region None

			else
			{
				if (Input.GetKeyDown(KeyCode.Backspace))
					tool.DeselectObject();
			}

			#endregion
		}
#endregion
	}
}