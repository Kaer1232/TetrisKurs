using System.Globalization;

namespace TetrisKurs.Base
{
    class VisabilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                return isVisible;  // Возвращаем true или false
            }
            return false; // По умолчанию скрываем
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                return isVisible; // Преобразуем обратно в тип bool
            }
            return false;
        }
    }
}
