using System;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace 阻止进入神庙
{

	internal class Config
	{
        [JsonProperty("检测间隔(ms)")]
        public int checkTime = 1000;
        [JsonProperty("世纪之花前禁止进入")]
        public bool plantBoss = true;

        [JsonProperty("三王前禁止进入")]
        public bool threeBoss = true;

        [JsonProperty("肉前禁止进入")]
        public bool hardMode = true;
        [JsonProperty("杀死试图进入的玩家")]
        public bool kill = true;

        public const string DefaultPath = "tshock/阻止进入神庙配置.json";

        public void Save()
		{
			using (StreamWriter streamWriter = new StreamWriter(DefaultPath))
			{
				streamWriter.WriteLine(JsonConvert.SerializeObject(this, Formatting.Indented));
			}
		}

		public static Config GetConfig()
		{
			Config config = new Config();
			bool flag = !File.Exists(DefaultPath);
			Config result;
			if (flag)
			{
				config.Save();
				result = config;
			}
			else
			{
				using (StreamReader streamReader = new StreamReader(DefaultPath))
				{
					config = JsonConvert.DeserializeObject<Config>(streamReader.ReadToEnd());
				}
				result = config;
			}
			return result;
		}

	}
}
