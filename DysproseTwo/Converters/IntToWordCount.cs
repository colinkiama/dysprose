using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DysproseTwo.Converters
{
    class IntToWordCount:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            StringBuilder sb = new StringBuilder();
            if (value is int num)
            {
                sb.Append(num);
            }
            else
            {
                sb.Append(0);
            }
            sb.Append(" Words");
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
