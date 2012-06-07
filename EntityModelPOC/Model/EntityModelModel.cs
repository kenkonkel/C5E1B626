using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModelPOC.Transport.EntityModel;

namespace EntityModelPOC.Model
{
	public class EntityModelModel
	{
		public IList<EntityResource> GetMeSomeManufacturers
		{
			get
			{
				return new ApplicationService.EntityModelApplicationService().GetEntityModelManufacturers();				
			}
		}
	}
}
