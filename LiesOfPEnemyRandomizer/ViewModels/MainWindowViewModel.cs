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

        private bool _outerStationBossSkip;
        public bool OuterStationBossSkip
        {
            get => _outerStationBossSkip;
            set
            {
                if (_outerStationBossSkip != value)
                {
                    _outerStationBossSkip = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includePuppets;
        public bool IncludePuppets
        {
            get => _includePuppets;
            set
            {
                if (_includePuppets != value)
                {
                    _includePuppets = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeCarcass;
        public bool IncludeCarcass
        {
            get => _includeCarcass;
            set
            {
                if (_includeCarcass != value)
                {
                    _includeCarcass = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeReborner;
        public bool IncludeReborner
        {
            get => _includeReborner;
            set
            {
                if (_includeReborner != value)
                {
                    _includeReborner = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeMiniBossStalker;
        public bool IncludeMiniBossStalker
        {
            get => _includeMiniBossStalker;
            set
            {
                if (_includeMiniBossStalker != value)
                {
                    _includeMiniBossStalker = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeMiniBossPuppet;
        public bool IncludeMiniBossPuppet
        {
            get => _includeMiniBossPuppet;
            set
            {
                if (_includeMiniBossPuppet != value)
                {
                    _includeMiniBossPuppet = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeBosses;
        public bool IncludeBosses
        {
            get => _includeBosses;
            set
            {
                if (_includeBosses != value)
                {
                    _includeBosses = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeMiniBossReborner;
        public bool IncludeMiniBossReborner
        {
            get => _includeMiniBossReborner;
            set
            {
                if (_includeMiniBossReborner != value)
                {
                    _includeMiniBossReborner = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeMiniBossCarcass;
        public bool IncludeMiniBossCarcass
        {
            get => _includeMiniBossCarcass;
            set
            {
                if (_includeMiniBossCarcass != value)
                {
                    _includeMiniBossCarcass = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            ButtonRandomizedClicked = new RelayCommand(OnButtonRandomizedClicked);
            IncludePuppets = true;
            IncludeCarcass = true;
            IncludeReborner = true;
            IncludeMiniBossStalker = true;
            IncludeMiniBossPuppet = true;
            IncludeBosses = false;
            IncludeMiniBossReborner = false;
            IncludeMiniBossCarcass = false;
            WanderingBoss = false;
            ScaleBossLvl = true;


        }


        async void OnButtonRandomizedClicked()
        {
            Randomizer randomizer = new Randomizer(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, IncludeBosses, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss, WanderingBossChance);
            //Randomizer randomizer = new Randomizer(true, true, true, true, true, false, false, false, false, 0.00f);
            randomizer.ScaleBosses = ScaleBossLvl;
            randomizer.skipChp1Boss = OuterStationBossSkip;

            int mySeed;

            if (!String.IsNullOrEmpty(Seed) && int.TryParse(Seed, out mySeed))
            {
                Seed = mySeed.ToString();
                await randomizer.RandomizeEnemies(mySeed, OuterStationBossSkip);
                return;
            }

            mySeed = randomizer.GenerateSeed();
            Seed = mySeed.ToString();
           
            await randomizer.RandomizeEnemies(mySeed, OuterStationBossSkip);


        }

    }
}







