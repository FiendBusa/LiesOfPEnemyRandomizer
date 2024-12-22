using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace LiesOfPEnemyRandomizer.src
{
    public class SoundHandler
    {
        public void PlayResourceSound(string resourceName)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if(stream == null) { Debug.WriteLine(resourceName + "not found"); return; }
                    using (SoundPlayer player = new SoundPlayer(stream))
                    {
                        player.Play();
                    }
                }
            }catch (Exception ex) { Debug.WriteLine(ex.Message); return; }
        }
    }
}