using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EntityModelPOC.Model;
using EntityModelPOC.Transport.EntityModel;

namespace EntityModelPOC.UI
{
	class EntityResourceDisplayNameConverter : IValueConverter
	{
		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var resource = value as EntityResource;
			if (resource != null)
				return resource.DisplayName();

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
