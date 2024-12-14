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
        private bool _scaleBossLvl;
        public bool ScaleBossLvl
        {
            get => _scaleBossLvl;
            set
            {
                if (_scaleBossLvl != value)
                {
                    _scaleBossLvl = value;
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
           
            Randomizer randomizer = new Randomizer(true, true, true, true, true, false, false, false, false, 0.00f);
            randomizer.ScaleBosses = ScaleBossLvl;

            int mySeed;

            if (!String.IsNullOrEmpty(Seed) && int.TryParse(Seed, out mySeed))
            {
                Seed = mySeed.ToString();
                randomizer.RandomizeEnemies(mySeed);
                return;
            }

            mySeed = randomizer.GenerateSeed();
            Seed = mySeed.ToString();
           
            randomizer.RandomizeEnemies(mySeed);


        }

    }
}







