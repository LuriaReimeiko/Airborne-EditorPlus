using Il2Cpp;
using MelonLoader;
using HarmonyLib;

namespace Airborne_EditorPlus
{
#region HandleKeybinds Patch

	[HarmonyPatch(typeof(EditorControls), nameof(EditorControls.HandleKeybinds))]
	class Patch_HandleKeybinds
	{
		static bool Prefix()
		{
			return false; // Thanks ChatGPT
		}
	}

#endregion

}