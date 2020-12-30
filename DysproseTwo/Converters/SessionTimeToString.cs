using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DysproseTwo.Converters
{
    class SessionTimeToString:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string formatString;
            if (value is TimeSpan sessionTime)
            {
                if (sessionTime.Hours > 0)
                {
                    formatString = @"hh\:mm\:ss";
                }
                else
                {
                    formatString = @"mm\:ss";
                }
            }
            else
            {
                formatString = "No Time Set";
            }
            return sessionTime.ToString(formatString);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
