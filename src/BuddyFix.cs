using BepInEx;
using BepInEx.Configuration;
using Logger;
using RoR2.ContentManagement;
using RoR2BepInExPack.GameAssetPaths.Version_1_39_0;
using UnityEngine;

[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace BuddyFix;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public sealed class BuddyFix : BaseUnityPlugin
{
	public const string PluginGUID = PluginAuthor + "." + PluginName;
	public const string PluginAuthor = "Onyx";
	public const string PluginName = "BuddyFix";
	public const string PluginVersion = "1.0.0";

	public static BuddyFix Instance;
	public static ConfigEntry<string> BuddyName { get; set; }

	public void Awake()
	{
		Log.Init(Logger);
		Instance = SingletonHelper.Assign(Instance, this);
		Options.Init();

		AssetAsyncReferenceManager<GameObject>.LoadAsset(new(RoR2_DLC3_FriendUnit.FriendUnitBody_prefab)).Completed += (x) =>
		{
			Transform hitbox = x.Result.transform.Find("ModelBase/mdlDefectiveUnit (1)/Hitbox");
			if (hitbox)
			{
				hitbox.localPosition = new Vector3(0, -1, 0.1f);
				hitbox.localScale = new Vector3(2.5f, 2.5f, 2.5f);
			}

			R2API.LanguageAPI.AddOverlay("FRIENDUNIT_BODY_NAME", BuddyName.Value);
		};
	}
}
