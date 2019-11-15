using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Model
{
    public class GlobalSettings
    {
        private double fontSize;
        private DysproseSessionSettings _sessionSettings;

        public double FontSize { get => fontSize; set => fontSize = value; }
        public DysproseSessionSettings SessionSettings
        {
            get => _sessionSettings;
            set
            {
                _sessionSettings = value;
                SessionSettingsUpdated?.Invoke(this, _sessionSettings);
            }
        }

        public event EventHandler<DysproseSessionSettings> SessionSettingsUpdated;

    }
}
