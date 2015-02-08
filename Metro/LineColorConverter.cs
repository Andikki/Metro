using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Metro
{
    public class LineColorConverter : IValueConverter
    {
        public SolidColorBrush[] BrushesArray { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var lineId = (int)value;
            if (BrushesArray != null && BrushesArray.Length >= lineId && lineId > 0)
            {
                return BrushesArray[lineId - 1];
            }
            else
            {
                return Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
