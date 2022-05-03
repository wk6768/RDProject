using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RDProject.Converters
{
    public class StatusToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            string Color;
            switch (status)
            {
                case 1:
                    Color = "Blue";
                    break;
                case 2:
                    Color = "Red";
                    break ;
                case 3:
                    Color = "Green";
                    break;
                default:
                    Color = "Black";
                    break;
            }
            return Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
