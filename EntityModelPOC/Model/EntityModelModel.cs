using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EntityModelPOC.Transport.EntityModel;

namespace EntityModelPOC.Model
{
	public class EntityModelModel : INotifyPropertyChanged
	{
		enum WindowDisplayType{Manufacturer, Vendor}

		private WindowDisplayType WindowType { get; set; }

		public IList<EntityResource> GetMeSomeManufacturers
		{
			get
			{
				WindowType = WindowDisplayType.Manufacturer; 
				return new ApplicationService.EntityModelApplicationService().GetEntityModelManufacturers();				
			}
		}

		public IList<EntityResource> GetMeSomeVendors
		{
			get
			{
				WindowType = WindowDisplayType.Vendor;
				return new ApplicationService.EntityModelApplicationService().GetEntityModelVendors();
			}
		}

		public ImageSource WindowIcon
		{
			//    new BitmapImage(new Uri("pack://application:,,,/IQ.Core.Resources;component/Images/windowbuttons/Close_Mouseover.png"));
			get { return new BitmapImage(new Uri("pack://application:,,,../UI/" + WindowType.ToString() + ".png")); }
		}

		public string WindowTitle
		{
			get { return WindowType.ToString(); }
		}

		public IList<EntityResource> RecordSet
		{
			get { return WindowType == WindowDisplayType.Manufacturer ? GetMeSomeManufacturers : GetMeSomeVendors; }
		}

		public void LoadSwitch()
		{
			WindowType = WindowType == WindowDisplayType.Manufacturer? WindowDisplayType.Vendor : WindowDisplayType.Manufacturer;
			SendPropertyChanged("RecordSet");
			SendPropertyChanged("WindowTitle");
			SendPropertyChanged("WindowIcon");
		}

		void SendPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
		#region Implementation of INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
