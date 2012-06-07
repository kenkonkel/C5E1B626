using System.Collections.Generic;

namespace EntityModelPOC.Transport.EntityModel
{
	public class LocationResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string StateName { get; set; }
		public string StateCode { get; set; }
		public string CountryName { get; set; }
		public string CountryCode { get; set; }
		public string Zip { get; set; }

		public IEnumerable<PhoneNumberResource> PhoneNumbers { get; set; }
	}
}