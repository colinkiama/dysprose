using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DysproseTwo.Converters
{
    class SessionStateToVisibility:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            Visibility isVisible = Visibility.Collapsed;
            if (value is DysproseSessionState sessionState)
            {
                switch (sessionState)
                {
                    case DysproseSessionState.Stopped:
                        isVisible = Visibility.Collapsed; 
                        break;
                    default:
                        isVisible = Visibility.Visible;
                        break;
                }
            }

            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
