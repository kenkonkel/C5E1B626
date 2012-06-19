using System.Collections.Generic;

namespace EntityModelPOC.Transport.EntityModel
{
	public class LocationDetailsResource
	{
		public string LocationType { get; set; }
		public string LocationSubType { get; set; }
		public AddressResource Address { get; set; }
		public IEnumerable<ContactResource> Contacts { get; set; }
	}
}