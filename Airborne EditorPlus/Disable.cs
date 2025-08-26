using Il2Cpp;
using MelonLoader;
using HarmonyLib;

namespace Airborne_EditorPlus
{
	#region EditorControls.HandleKeybinds Patch

	[HarmonyPatch(typeof(EditorControls), nameof(EditorControls.HandleKeybinds))]
	class Patch_HandleKeybinds
	{
		static bool Prefix()
		{
			return !EditorPlus.enabled;
		}
	}

#endregion

#region EditorControls.Update Patch

	[HarmonyPatch(typeof(EditorControls), nameof(EditorControls.Update))]
	class Patch_Update
	{
		static readonly VanillaBehaviour vanillaBehaviour = new();
		static bool Prefix()
		{
			if (EditorPlus.enabled)
			{
				vanillaBehaviour.OverrideUpdate();
				return false;
			}
			return true;
		}
	}

#endregion

}