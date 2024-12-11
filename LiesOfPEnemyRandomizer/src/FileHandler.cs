using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using UAssetAPI;
using System.Reflection.Metadata;
using System.Reflection;
using Tmds.DBus.Protocol;
using System.Diagnostics;


namespace LiesOfPEnemyRandomizer.src
{
    public enum MapName
    {
        LD_Outer_Station_DSN,
        LV_Outer_CentralStatinB_DSN,
        LV_Inner_UpperStreet_DSN,
        LV_Inner_Cathedral_DSN,
        LV_Outer_Exhibition_DSN,
        LV_Inner_Factory_DSN,
        LV_Outer_Grave_DSN,
        LV_Krat_Old_Town_DSN,
        LV_Outer_Underdark_A_DSN,
        LV_Outer_Underdark_DSN,
        LV_Krat_EastEndWard_DSN,
        LV_Monastery_A_DSN,
        LV_Monastery_B_DSN
    };
    public class FileHandler
    {
        public readonly string tempPath;

        public readonly Dictionary<string, string[]> pakChunk0_s4;
        public readonly Dictionary<string, string[]> pakChunk2_s3;
        public readonly Dictionary<string, string[]> pakChunk2_s4;

        List<Dictionary<string, string[]>> pakChunkCollection;

        public readonly string[] pakBaseDirectory = {
            "pakchunk0_s4-WindowsNoEditor_P",
            "pakchunk2_s3-WindowsNoEditor_P",
            "pakchunk2_s4-WindowsNoEditor_P"
        };

             


        public FileHandler(string tempPath)
        {

            this.tempPath = Path.Combine(tempPath, "fbloprandomizer");


            pakChunk0_s4 = new Dictionary<string, string[]>
            {
                {Path.Combine(this.tempPath, pakBaseDirectory[0],"LiesofP\\Content\\Blueprints\\LevelObjectBP"), new string[] {"BP_NpcSpot.uasset","BP_ItemSpot.uasset","BP_BossRoomSpot.uasset","BP_NpcSpot.uexp", "BP_ItemSpot.uexp", "BP_BossRoomSpot.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[0],"LiesofP\\Content\\ContentInfo\\InfoAsset"), new string[] {"NPCInfo.uasset", "NPCInfo.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[0],"LiesofP\\Content\\MapRelease\\LV_OuterKrat\\LV_CentralStation"), new string[] { "LD_Outer_Station_DSN.umap", "LD_Outer_Station_DSN.uexp" } },
            };

                        pakChunk2_s3 = new Dictionary<string, string[]>
            {
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\Blueprints\\LevelObjectBP"), new string[] { "BP_NpcSpot.uasset", "BP_ItemSpot.uasset", "BP_BossRoomSpot.uasset", "BP_NpcSpot.uexp", "BP_ItemSpot.uexp", "BP_BossRoomSpot.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\ContentInfo\\InfoAsset"), new string[] { "NPCInfo.uasset", "NPCInfo.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_CentralStation_B"), new string[] { "LV_Outer_CentralStatinB_DSN.umap", "LV_Outer_CentralStatinB_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_InnerKrat"), new string[] { "LV_Inner_UpperStreet_DSN.umap", "LV_Inner_UpperStreet_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_Krat_Cathedral"), new string[] { "LV_Inner_Cathedral_DSN.umap", "LV_Inner_Cathedral_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_Krat_Exhibition"), new string[] { "LV_Outer_Exhibition_DSN.umap", "LV_Outer_Exhibition_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_Krat_Factory"), new string[] { "LV_Inner_Factory_DSN.umap", "LV_Inner_Factory_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[1],"LiesofP\\Content\\MapRelease\\LV_Krat_Grave"), new string[] { "LV_Outer_Grave_DSN.umap", "LV_Outer_Grave_DSN.uexp" } },
            };

                        pakChunk2_s4 = new Dictionary<string, string[]>
            {
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\Blueprints\\LevelObjectBP"), new string[] { "BP_NpcSpot.uasset", "BP_ItemSpot.uasset", "BP_BossRoomSpot.uasset", "BP_NpcSpot.uexp", "BP_ItemSpot.uexp", "BP_BossRoomSpot.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\ContentInfo\\InfoAsset"), new string[] { "NPCInfo.uasset", "NPCInfo.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\MapRelease\\LV_Krat_Old_Town"), new string[] { "LV_Krat_Old_Town_DSN.umap", "LV_Krat_Old_Town_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\MapRelease\\LV_Krat_Underdark"), new string[] { "LV_Outer_Underdark_A_DSN.umap", "LV_Outer_Underdark_A_DSN.uexp", "LV_Outer_Underdark_DSN.umap", "LV_Outer_Underdark_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\MapRelease\\LV_OuterKrat\\LV_Krat_Outer_EastEndWard"), new string[] { "LV_Krat_EastEndWard_DSN.umap", "LV_Krat_EastEndWard_DSN.uexp" } },
                {Path.Combine(this.tempPath, pakBaseDirectory[2],"LiesofP\\Content\\MapRelease\\LV_Zone_D"), new string[] { "LV_Monastery_A_DSN.umap", "LV_Monastery_A_DSN.uexp", "LV_Monastery_B_DSN.umap", "LV_Monastery_B_DSN.uexp" } },
            };


            pakChunkCollection = new List<Dictionary<string, string[]>> { pakChunk0_s4, pakChunk2_s3, pakChunk2_s4 };

             

        }
        public async Task<bool> CopyAndWriteAssets(string[] assets, List<Dictionary<string, string[]>> pakChunkCollection)
        {
            
            try
            {
                //assets = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (var j in pakChunkCollection)
                {

                    foreach (var k in j.Keys)
                    {
                        Directory.CreateDirectory(k);
                        for (int i = 0; i < j[k].Length; i++)
                        {
                            //string? asset = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Equals(pakChunk0_s4[k][i])).FirstOrDefault();
                            string? asset = assets.FirstOrDefault(x => x.Contains(j[k][i], StringComparison.OrdinalIgnoreCase));
                            if (asset == null) { continue; }

                            using (Stream? rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(asset))
                            {
                                if (rs == null) { continue; }

                                var destFile = Path.Combine(k, j[k][i]);

                                using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    await rs.CopyToAsync(fs);
                                    


                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) { return false; }
            return true;
        }
               
            

            
        
        public async Task<bool> CopyResource(string? resource, string path, string indexOf)
        {      
                if (resource == null) { return false; }
                try
                {
                    string? asset = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains(resource, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (asset == null) { return false; }


                    using (Stream? rs = Assembly.GetExecutingAssembly().GetManifestResourceStream(asset))
                    {
                        if (rs == null) { return false; }

                    var destFile = Path.Combine(path, resource.Substring(resource.IndexOf(indexOf)));

                        using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.ReadWrite))
                        {
                            await rs.CopyToAsync(fs);
                            
                        }
                    }
                }

                catch (Exception ex) { return false; }

            return true;
        }

        public async Task<bool> UnrealPak(string[] baseDirectories, string outputDir)
        {
            for (int i = 0; i < baseDirectories.Length; i++)
            {
                string pakFolder = baseDirectories[i];
                string unrealpakExe = Path.Combine(tempPath, "UnrealPak.exe");
                if (!File.Exists(unrealpakExe))
                {
                    Console.WriteLine("UnrealPak.exe not found.");
                    return false;
                }

                // Generate filelist.txt
                string filelistPath = Path.Combine(tempPath, "filelist.txt");
                string sourcePattern = Path.Combine(pakFolder, "*.*");
                string targetPattern = Path.Combine("..\\..\\..", "*.*");
                File.WriteAllText(filelistPath, $"\"{sourcePattern}\" \"{targetPattern}\"");

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Specify output pak file path in the output directory
                string pakName = Path.GetFileName(pakFolder) + ".pak"; // Use the folder name as the pak file name
                string outputPakPath = Path.Combine(outputDir, pakName);

                // Arguments for UnrealPak.exe
                string arguments = $"\"{outputPakPath}\" -create=\"{filelistPath}\" -compress";

                // Run UnrealPak.exe
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = unrealpakExe,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                try
                {
                    using (Process process = Process.Start(processInfo))
                    {
                        string output = await process.StandardOutput.ReadToEndAsync();
                        string error = await process.StandardError.ReadToEndAsync();

                        process.WaitForExit();

                        Console.WriteLine("UnrealPak Output:");
                        Console.WriteLine(output);

                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("UnrealPak Errors:");
                            Console.WriteLine(error);
                            return false; // Exit on error
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while executing UnrealPak.exe: {ex.Message}");
                    return false;
                }
            }

            return true;
        }









        public async Task<string[]?> GenerateBaseTempFiles()
        {


            if (string.IsNullOrEmpty(tempPath)) return null;
            DeleteTempDir();

            string[] assets = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            bool result = await CopyAndWriteAssets(assets, pakChunkCollection);
            //if (!result) return return pakChunkFiles;

            string? unrealpak = assets.FirstOrDefault(x => x.Contains("unrealpak.exe", StringComparison.OrdinalIgnoreCase));
            //if (unrealpak == null){ return null; }

            string? mapping = assets.FirstOrDefault(x => x.Contains("mappings.usmap", StringComparison.OrdinalIgnoreCase));
            //if (unrealpak == null) { return null; }

            bool resultUnrealExe = await CopyResource(unrealpak, tempPath, "UnrealPak.exe");
            bool resultMappingFile = await CopyResource(mapping, tempPath, "Mappings.usmap");

            if (!(resultUnrealExe) || (!(resultMappingFile))) return null;

            return Directory.GetFiles(tempPath, "*.umap", SearchOption.AllDirectories);
        }









        
       

        bool DeleteTempDir()
        {
            try
            {
                Directory.Delete(tempPath, true);
                return true;
            }
            catch (DirectoryNotFoundException)
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
    
}
