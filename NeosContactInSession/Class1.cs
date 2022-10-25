using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Reflection;
using System.Net;
using NeosModLoader;
using CloudX;
using FrooxEngine;
using FrooxEngine.UIX;
using HarmonyLib;

namespace NeosDllLoader
{
    public class Class1 : NeosMod
    {
        public override string Name => "ContactsInSession";
        public override string Author => "NVPN";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/Falkitop";

        [AutoRegisterConfigKey]
        public static readonly ModConfigurationKey<BaseX.color> KEY_COLOR = new ModConfigurationKey<BaseX.color>("Color", "TextColor");
        public static ModConfiguration config;
        public override void OnEngineInit()
        {
            Msg("OnEngineInit");
            config = GetConfiguration();
            config.Set(KEY_COLOR, BaseX.color.Red);
            Harmony harmony = new Harmony("com.nordvpn.patch");
            harmony.PatchAll();
        }
        public static T CastTo<T>(object i) { return (T)i; }
    }


    [HarmonyPatch(typeof(SessionUserController), "Create")]
    public class InventoryBrowserDarkPatch
    {
        [HarmonyPostfix]
        static void InvPatch(ref SessionUserController __result, User user)
        {
            try
            {
                if (FrooxEngine.Engine.Current.WorldAnnouncer.Cloud.Friends.IsFriend(user.UserID))
                Class1.CastTo<SyncRef<Text>>(__result.GetSyncMember(3)).Target.Color.Value = Class1.config.GetValue(Class1.KEY_COLOR);
            }
            catch (Exception ex)
            {
                Class1.Msg(ex);
            }
            
        }
    }

}
