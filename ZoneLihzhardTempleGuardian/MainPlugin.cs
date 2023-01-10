using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rests;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.Localization;
using Terraria.WorldBuilding;
using Terraria.IO;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using OTAPI;
using Terraria.GameContent.Creative;
using System.Xml.Linq;

namespace 阻止进入神庙
{
    [ApiVersion(2, 1)]
    public class 阻止进入神庙 : TerrariaPlugin
    {

        public override string Author
        {
            get
            {
                return "Cai";
            }
        }


        public override string Description
        {
            get
            {
                return "阻止进入神庙";
            }
        }

        public override string Name
        {
            get
            {
                return "阻止进入神庙";
            }
        }

        public override Version Version
        {
            get
            {
                return new Version(1, 0, 0, 0);
            }
        }
        
        public 阻止进入神庙(Terraria.Main game) : base(game)
        {
            Utils.Config = Config.GetConfig();
        }
        public override void Initialize()
        {
            WorldGen.Hooks.OnWorldLoad += Worldload;
            //ServerApi.Hooks.GameWorldConnect.Register(this, GameInitialize);
            GeneralHooks.ReloadEvent += OnReload;
        }


        private void Worldload()
        {
            Thread thread = new Thread(CheckThread);
            thread.IsBackground = true;
            thread.Start();
        }

        private void OnReload(ReloadEventArgs args)
        {
            Utils.Config = Config.GetConfig();
            TShock.Log.Info("[阻止进入神庙]: 配置文件已重载！");
            args.Player.SendWarningMessage("[阻止进入神庙]: 配置文件已重载！");
        }

        private static void CheckThread()
        {
            while (true)
            {           
                try
                {
                    Thread.Sleep(Utils.Config.checkTime);
                    foreach (var plr in TShock.Players)
                    {
                        if (plr != null && plr.Active && !plr.Dead && !plr.HasPermission("ZoneLihzhardTempleCheck.ignore"))
                        {
                            //Console.WriteLine("是否神庙:" + Utils.isInZoneLihzhardTemple(plr));
                            //Console.WriteLine("是否越进度:" + !Utils.checkProgress());
                            //Console.WriteLine("是否杀死:" + Utils.Config.kill);
                            if (Utils.isInZoneLihzhardTemple(plr) && !Utils.checkProgress() && !plr.Dead)
                            {
                                if (Utils.Config.kill)
                                {
                                    plr.SendErrorMessage("有一股强大的力量阻止你进入神庙!");
                                    plr.KillPlayer();
                                }
                                else
                                {
                                    plr.SendErrorMessage("有一股强大的力量阻止你进入神庙!");
                                    plr.Teleport(Main.spawnTileX * 16, (Main.spawnTileY * 16) - 48);
                                }
                            }
                        }
                    }
                }

                catch
                {
                }
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                WorldGen.Hooks.OnWorldLoad -= Worldload;
                GeneralHooks.ReloadEvent -= OnReload;
                //ServerApi.Hooks.GameInitialize.Deregister(this, GameInitialize);
            }
            base.Dispose(disposing);
        }
    }
}
