using System.Collections.Generic;

namespace EntityModelPOC.Transport.EntityModel
{
	public class ContactResource
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IEnumerable<PhoneNumberResource> PhoneNumbers { get; set; }
	}
}