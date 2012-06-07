using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModelPOC.Transport.EntityModel
{
	public class EntityResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<EntityRoleResource> Roles { get; set; }
		public Dictionary<string, string> Attributes { get; set; }
		public List<int> Locations { get; set; }
		public string SortName { get; set; }

		public int Version { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? LastModifiedOn { get; set; }
	}
}
