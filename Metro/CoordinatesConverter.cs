using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Metro
{
    public class CoordinatesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dimension = (double)values[0];
            var coord = (int)values[1];
            var offsetDimension = 0d;
            if (values.Length > 2 && values[2] is double)
            {                
                offsetDimension = (double)values[2];
            }
            return dimension * coord / 100 - offsetDimension;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
