using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using TShockAPI;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;
using System.Diagnostics;


namespace 阻止进入神庙
{
    internal class Utils
    {
        public static Config Config;

        public static bool isInZoneLihzhardTemple(TSPlayer plr)
        {
            return Main.player[plr.Index].ZoneLihzhardTemple;
        }

        public static bool checkProgress()
        {
            if (Config.hardMode)
            {
                if (!Main.hardMode)
                    return false;
            }
            if (Config.threeBoss)
            {
                if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                    return false;
            }
            if (Config.plantBoss)
            {
                if (!NPC.downedPlantBoss)
                    return false;
            }
            return true;
        }

    }
}
    
