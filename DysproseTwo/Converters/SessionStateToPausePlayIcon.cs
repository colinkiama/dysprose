using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace DysproseTwo.Converters
{
    class SessionStateToPausePlayIcon:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SymbolIcon icon = new SymbolIcon(Symbol.Play);
            if (value is DysproseSessionState sessionState)
            {
                switch (sessionState)
                {
                    case DysproseSessionState.InProgress:
                        icon = new SymbolIcon(Symbol.Pause);
                        break;
                }
            }

            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
