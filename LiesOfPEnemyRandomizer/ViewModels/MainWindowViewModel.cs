using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
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
       
        public ICommand ButtonRandomizedClicked { get; }

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
        
            
        

        public MainWindowViewModel()
        {
            ButtonRandomizedClicked = new RelayCommand(OnButtonRandomizedClicked);
           
        }


        void OnButtonRandomizedClicked()
        {
            Usmap mapping = new Usmap("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\Mappings.usmap");
            UAsset myAsset = new UAsset("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\LV_Inner_UpperStreet_DSN.umap",
                EngineVersion.VER_UE4_27, mapping);

            NormalExport? npc = (NormalExport?) myAsset.Exports.Where(x => x.ObjectName.ToString() == "Npc-LV_Inner_UpperStreet_DSN-1").FirstOrDefault();

           if(npc == null) { return; }


            //NamePropertyData test = new NamePropertyData() { RawValue = "TEST" };
            //npc.Data[5].RawValue = test;
            //myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");

            npc.Data[5].RawValue = FName.FromString(myAsset, "Npc-LV_Inner_UpperStreet_DSN-2");
            myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
            //     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");


            //if(npc is NormalExport export)
            // {
            //     export.Data[5] = new NamePropertyData() { RawValue = "Npc-LV_Inner_UpperStreet_DSN-2" };
            //     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
            // }











        }

    }
}
