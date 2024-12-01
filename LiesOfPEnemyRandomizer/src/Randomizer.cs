using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiesOfPEnemyRandomizer.src
{
    public class Randomizer
    {
        //Puppet & Carcass 
        public bool IncludePuppets { get; set; } = true;
        public bool IncludeCarcass { get; set; } = true;
        public bool FactionProtectionPuppetCaracss { get; set; }
        public bool WanderingBoss { get; set; }
        public float WanderingBossChance { get; set; } = 0.0f;
        public bool BossProtection { get; set; } = true;
        public bool StalkerProtection { get; set; } = true;
        public bool LargeMiniBossProtection { get; set; } = true;




        public void RandomizeEnemies(bool IncludePuppets, bool IncludeCarcass, bool WanderingBoss, float WanderingBossChance, int Seed)
        {
            this.IncludePuppets = IncludePuppets;
            this.IncludeCarcass = IncludeCarcass;
            this.WanderingBoss = WanderingBoss;
            this.WanderingBossChance = WanderingBossChance;

            Random random = new Random(Seed);

        }
        public static int GenerateSeed()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next();
        }
        
        
    }
}
