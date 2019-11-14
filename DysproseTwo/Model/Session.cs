using DysproseTwo.Enums;
using DysproseTwo.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DysproseTwo.Model
{
    public class Session
    {
        SessionSettings Settings;
        string TextData;
        DispatcherTimer timer;

        public Session()
        {
            Settings = new SessionSettings { FontSize = 15, SessionLength = new TimeSpan(0, 1, 0) };
        }
    }
}
