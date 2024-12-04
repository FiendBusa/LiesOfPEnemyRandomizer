using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using LiesOfPEnemyRandomizer.src;
using System;
using System.Linq;
using System.Windows.Input;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace LiesOfPEnemyRandomizer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        public ICommand ButtonRandomizedClicked { get; set; }

        private float _wanderingBossChance;
        public float WanderingBossChance
        {
            get => _wanderingBossChance;
            set
            {
                if (_wanderingBossChance != value)
                {
                    _wanderingBossChance = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _wanderingBoss;
        public bool WanderingBoss
        {
            get => _wanderingBoss;
            set
            {
                if (_wanderingBoss != value)
                {
                    _wanderingBoss = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _randomizePuppetsCarcass;
        public bool RandomizePuppetsCarcass
        {
            get => _randomizePuppetsCarcass;
            set
            {
                if (_randomizePuppetsCarcass != value)
                {
                    _randomizePuppetsCarcass = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _factionProtection;
        public bool FactionProtection
        {
            get => _factionProtection;
            set
            {
                if (_factionProtection != value)
                {
                    _factionProtection = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _seed;
        public string? Seed
        {
            get => _seed;
            set
            {
                if (_seed != value)
                {
                    _seed = value;
                    OnPropertyChanged();
                }
            }
        }




        public MainWindowViewModel()
        {
            ButtonRandomizedClicked = new RelayCommand(OnButtonRandomizedClicked);

        }


        void OnButtonRandomizedClicked()
        {
            Randomizer randomizer = new Randomizer(true, true, true, true, false, false, false, false, false, 0.00f);
            int result;
            bool hasSeed = int.TryParse(Seed, out result);

            int mySeed = randomizer.GenerateSeed();
            randomizer.RandomizeEnemies(mySeed);


        }

    }
}



 // randomizer.RandomizeEnemies(true, true, true, WanderingBossChance, seed);

            // Usmap mapping = new Usmap("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\Mappings.usmap");
            // UAsset myAsset = new UAsset("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\LV_Inner_UpperStreet_DSN.umap",
            //     EngineVersion.VER_UE4_27, mapping);

            // NormalExport? npc = (NormalExport?) myAsset.Exports.Where(x => x.ObjectName.ToString() == "Npc-LV_Inner_UpperStreet_DSN-1").FirstOrDefault();

            //if(npc == null) { return; }


            //NamePropertyData test = new NamePropertyData() { RawValue = "TEST" };
            //npc.Data[5].RawValue = test;
            //myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");

            //npc.Data[5].RawValue = FName.FromString(myAsset, "Npc-LV_Inner_UpperStreet_DSN-2");
            //myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
            //     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");


            //if(npc is NormalExport export)
            // {
            //     export.Data[5] = new NamePropertyData() { RawValue = "Npc-LV_Inner_UpperStreet_DSN-2" };
            //     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
            // }









