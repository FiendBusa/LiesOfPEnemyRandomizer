using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using LiesOfPEnemyRandomizer.src;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Tmds.DBus.Protocol;
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

        private double _wanderingBossChance;
        public double WanderingBossChance
        {
            get => Math.Round(_wanderingBossChance, 2);
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
        private bool _randomizeDrops;
        public bool RandomizeDrops
        {
            get => _randomizeDrops;
            set
            {
                if (_randomizeDrops != value)
                {
                    _randomizeDrops = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _btnRandomizeEnabled;
        public bool BtnRandomizedEnabled
        {
            get => _btnRandomizeEnabled;
            set
            {
                if(_btnRandomizeEnabled != value)
                {
                    _btnRandomizeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _btnRandomizeText;
        public string BtnRandomizeText
        {
            get => _btnRandomizeText;
            set
            {
                if(_btnRandomizeText != value)
                {
                    _btnRandomizeText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _txtMain;
        public string TxtMain
        {
            get => _txtMain;
            set
            {
                if(_txtMain != value)
                {
                    _txtMain = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _randomizeDropsNpcImportantItemMaxPercent;
        public bool RandomizeDropsNpcImportantItemMaxPercent
        {
            get => _randomizeDropsNpcImportantItemMaxPercent;
            set
            {
                if (_randomizeDropsNpcImportantItemMaxPercent != value)
                {
                    _randomizeDropsNpcImportantItemMaxPercent = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _randomizeDropsNpcBalancePercent;
        public bool RandomizeDropsNpcBalancePercent
        {
            get => _randomizeDropsNpcBalancePercent;
            set
            {
                if (_randomizeDropsNpcBalancePercent != value)
                {
                    _randomizeDropsNpcBalancePercent = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _randomizeWorldItemWeaponDropStack;
        public bool RandomizeWorldItemWeaponDropStack
        {
            get => _randomizeWorldItemWeaponDropStack;
            set
            {
                if(_randomizeWorldItemWeaponDropStack != value)
                {
                    _randomizeWorldItemWeaponDropStack = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _randomizeWorldItemWeaponDropStackRate;
        public double RandomizeWorldItemWeaponDropStackRate
        {
            get => Math.Round(_randomizeWorldItemWeaponDropStackRate, 2);
       
            set
            {
                if (_randomizeWorldItemWeaponDropStackRate != value)
                {
                    _randomizeWorldItemWeaponDropStackRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _increaseBossAttributes;
        public bool IncreaseBossAttributes
        {
            get => _increaseBossAttributes;
            set
            {
                if (_increaseBossAttributes != value)
                {
                    _increaseBossAttributes = value;
                    OnPropertyChanged();
                }
            }
        }


        SoundHandler soundHandler;

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
            RandomizeDrops = true;
            BtnRandomizedEnabled = true;
            FactionProtection = true;
            RandomizeDropsNpcImportantItemMaxPercent = true;
            RandomizeDropsNpcBalancePercent = true;
            TxtMain = GlobalStrings.txtMainDefault;
            soundHandler = new SoundHandler();
            RandomizeWorldItemWeaponDropStack = false;
            IncreaseBossAttributes = true;
            

        }


        async void OnButtonRandomizedClicked()
        {
            BtnRandomizedEnabled = false;
            TxtMain = GlobalStrings.txtRandomizerStart;

            ItemDataBase itemsAll = new ItemDataBase();
            try
            {
                //SUPER EARLY NASTY TEST CODE FOR ITEM/NPC DROP LoL
                if (RandomizeDrops)
                {
                    string tempPath = System.IO.Path.GetTempPath();
                    FileHandler fileHandler = new FileHandler(tempPath);
                    await fileHandler.CopyResource(GlobalStrings.itemDB, tempPath, GlobalStrings.itemDB);

                    tempPath = Path.Combine(tempPath, GlobalStrings.itemDB);
                    itemsAll = new ItemDataBase();

                    if (File.Exists(tempPath))
                    {
                        itemsAll = ItemDataBase.LoadItems(tempPath);
                    }
                }


                //UNCOMMENT AFTER DONE TESTING ITEM RANDOMIZER
                Randomizer randomizer = new Randomizer(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, IncludeBosses, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss, WanderingBossChance, itemsAll, RandomizeDrops, FactionProtection, 
                    RandomizeDropsNpcBalancePercent, RandomizeDropsNpcImportantItemMaxPercent, RandomizeWorldItemWeaponDropStack, RandomizeWorldItemWeaponDropStackRate, IncreaseBossAttributes);

              
                randomizer.LogUpdated += (message) =>
                {
                
                    TxtMain = message;
                };


                randomizer.ScaleBosses = ScaleBossLvl;
                randomizer.skipChp1Boss = OuterStationBossSkip;

                int mySeed;

                if (!String.IsNullOrEmpty(Seed) && int.TryParse(Seed, out mySeed))
                {
                    Seed = mySeed.ToString();
                    await Task.Run(() => randomizer.RandomizeEnemies(mySeed, OuterStationBossSkip));
                    BtnRandomizedEnabled = true;
                    TxtMain = GlobalStrings.txtRandomizerFinish;
                    soundHandler.PlayResourceSound(GlobalStrings.sfxClock);
                    return;
                }

                mySeed = randomizer.GenerateSeed();
                Seed = mySeed.ToString();

                await Task.Run(() => randomizer.RandomizeEnemies(mySeed, OuterStationBossSkip));
                BtnRandomizedEnabled = true;
                TxtMain = GlobalStrings.txtRandomizerFinish;
                soundHandler.PlayResourceSound(GlobalStrings.sfxClock);

            }
            catch (Exception ex)
            {
                BtnRandomizedEnabled = true;
                TxtMain = $"Error: {ex.Message}";


            }
        }
       
    }
}


           









