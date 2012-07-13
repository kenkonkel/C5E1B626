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
	public class EntityModelModel
	{
		public enum EntityType { Vendor, Manufacturer }

		public EntityModelModel()
		{
			_records = new CollectionViewSource();
			_records.Filter += (sender, e) =>
			{
				var entity = e.Item as EntityResource;
				e.Accepted = string.IsNullOrWhiteSpace(_listFilter) || entity.Name.ToLower().Contains(_listFilter);
			};

			_addresses = new CollectionViewSource();

			// whenever the entity changes, go get the addresses.
			TypeDescriptor.GetProperties(this)["CurrentlySelectedEntity"].AddValueChanged(this, delegate
				{
					_addresses.Source = null;
					if (CurrentlySelectedEntity != null)
						_addresses.Source = GetAddressesForEntity(CurrentlySelectedEntity.Id);						
					
					_set("AddressRecordSet", _addresses.View);
				});

			SetProperties();
		}

		private IList<EntityResource> GetAddressesForEntity(int id)
		{
			var result = Task<IList<EntityResource>>.Factory.StartNew(() => new ApplicationService.EntityModelApplicationService().
			                                                   	GetLocations(CurrentlySelectedEntity.Id));

			return result.Result;
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

		// current item.
		public EntityResource CurrentlySelectedEntity { get; set; }

		private readonly CollectionViewSource _records;
		private readonly CollectionViewSource _addresses;

		private EntityType WindowType { get; set; }

		public IList<EntityResource> GetMeSomeManufacturers
		{
			get
			{
				return new ApplicationService.EntityModelApplicationService().GetEntityModelManufacturers();				
			}
		}

		public IList<EntityResource> GetMeSomeVendors
		{
			get
			{
				return new ApplicationService.EntityModelApplicationService().GetEntityModelVendors();
			}
		}

		public ImageSource WindowIcon { get; set; }
		public string WindowTitle { get; set; }
		public ICollectionView RecordSet { get; set; }
		public ICollectionView AddressRecordSet { get; set; }
		
		public void LoadSwitch()
		{
			WindowType = WindowType == EntityType.Manufacturer ? EntityType.Vendor : EntityType.Manufacturer;
			SetProperties();
		}

		private void SetProperties()
		{
			_records.Source = WindowType == EntityType.Manufacturer ? GetMeSomeManufacturers : GetMeSomeVendors;
			_set("RecordSet", _records.View);
			_set("WindowTitle", WindowType.ToString());
			_set("WindowIcon", new BitmapImage(new Uri("pack://application:,,,../UI/" + WindowType.ToString() + ".png")));
		}

		void _set(string propertyName, object value)
		{
			System.ComponentModel.TypeDescriptor.GetProperties(this)[propertyName].SetValue(this, value);
		}

		T _get<T>(string propertyName)
		{
			return (T)System.ComponentModel.TypeDescriptor.GetProperties(this)[propertyName].GetValue(this);
		}
	}
}
