using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IQ.EntityManager.DataService.Model;

namespace EntityModelPOC.Model
{
	public class EntityModelModel : INotifyPropertyChanged
	{
		public EntityModelModel()
		{
			_records = new CollectionViewSource();
			_records.Filter += (sender, e) =>
			{
				var entity = e.Item as EntityResource;
				e.Accepted = string.IsNullOrWhiteSpace(_listFilter) || entity.Name.ToLower().Contains(_listFilter);
			};
		}

		private string _listFilter = string.Empty;
		public string ListFilter 
		{ 
			get { return _listFilter; }
			set { 
				_listFilter = value.ToLower(); 
				_records.View.Refresh(); 
			} 
		}


		private readonly CollectionViewSource _records;
		
		enum WindowDisplayType { Manufacturer, Vendor }

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

		public ICollectionView RecordSet { 
			get
			{
				if (_records.Source == null)
				{
					_records.Source = WindowType == WindowDisplayType.Manufacturer ? GetMeSomeManufacturers : GetMeSomeVendors;
				}
				return _records.View;
			}
		}

		public void LoadSwitch()
		{
			WindowType = WindowType == WindowDisplayType.Manufacturer? WindowDisplayType.Vendor : WindowDisplayType.Manufacturer;
			_records.Source = null;
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
