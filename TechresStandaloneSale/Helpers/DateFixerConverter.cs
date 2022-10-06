using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TechresStandaloneSale.Helpers
{
    public class DateFixerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string va = (string)value;
            if (!string.IsNullOrEmpty(va))
            {
                return DateTime.Parse(va).ToString("yyyy/MM/dd");
            }
            else
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string)value);
        }
    }
}
