using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DysproseTwo.Converters
{
    class SessionStateToBool:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isEnabled = false;
            if (value is DysproseSessionState sessionState)
            {
                switch (sessionState)
                {
                    case DysproseSessionState.InProgress:
                        isEnabled = true;
                        break;
                }
            }

            return isEnabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
