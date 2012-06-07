using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EntityModelPOC.Transport.EntityModel;

namespace EntityModelPOC.UI
{
	/// <summary>
	/// Interaction logic for EntityResourceDetails.xaml
	/// </summary>
	public partial class EntityResourceDetails : UserControl
	{
		public static readonly DependencyProperty EntityResourceProperty =
			DependencyProperty.Register("EntityResource", typeof (EntityResource), typeof (EntityResourceDetails), new PropertyMetadata(default(EntityResource)));

		public EntityResource EntityResource
		{
			get { return (EntityResource) GetValue(EntityResourceProperty); }
			set { SetValue(EntityResourceProperty, value); }
		}
	
		public EntityResourceDetails()
		{
			DataContext = this;
			InitializeComponent();
		}

		public EntityResourceDetails(EntityResource entity) : this()
		{
			EntityResource = entity;
		}


	}
}
