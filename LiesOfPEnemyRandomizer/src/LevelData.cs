using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI;
using UAssetAPI.ExportTypes;

namespace LiesOfPEnemyRandomizer.src
{
    public class LevelData
    {
        public string Pchunk { get; set; }
        public string? NpcInfoAsset { get; set; }
        public string AssetName { get; set; }

        UAsset MyAsset { get; set; }

        UAsset NpcInfo { get; set; }

        List<NormalExport> Npc { get; set; }

        List<NpcData.NpcSpotData> ImportantNpcs { get; set; }

    }
}
