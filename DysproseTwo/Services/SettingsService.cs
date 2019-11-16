using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Services
{
    public static class SettingsService
    {
        public static void UpdateSessionSettings(DysproseSessionSettings sessionSettings)
        {
            
        }

        public static void UpdateGlobalSettings(DysproseGlobalSettings globalSessionSettings)
        {

        }
        public static event EventHandler SettingsUpdated;
    }
}
