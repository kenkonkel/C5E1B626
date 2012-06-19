using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EntityModelPOC.Transport.EntityModel;
using RestSharp;
using RestSharp.Deserializers;

namespace EntityModelPOC.ApplicationService
{
	public class EntityModelApplicationService
	{
		//private readonly string BASE_URI = System.Configuration.ConfigurationManager.AppSettings.Get("ENTITY_MANAGER_URI");
		private readonly string BASE_URI = "http://entitymanagerdev.iqmetrix.net/v1/";

		public IList<EntityResource> GetEntityModelManufacturers()
		{
			var request = new RestRequest { Resource = "manufacturers" };
			var response = Execute<List<Transport.EntityModel.EntityResource>>(request);

			return response.Data;
		}

		// STUFF TO CONNECT TO ENTITY MODEL.
		#region REST HELPERS

		public RestResponse<T> Execute<T>(RestRequest request) where T : new()
		{
			string resourceUri = request.Resource;
			var response = ClientFactory().Execute<T>(request);
			
			CheckResponseForError(response);

			return response;
		}

		private RestClient ClientFactory()
		{
			var client = new RestClient();
			client.BaseUrl = BASE_URI;
			client.AddHandler("application/json", new JsonDeserializer());
			client.UserAgent = string.Format("{0} {1}", GetType().FullName, GetType().Assembly.GetName().Version);
			return client;
		}

		private void CheckResponseForError(IRestResponse response)
		{
			if ((response.ResponseStatus == ResponseStatus.Error)
			  || (response.StatusCode != HttpStatusCode.OK
			  && response.StatusCode != HttpStatusCode.Created
			  && response.StatusCode != HttpStatusCode.Accepted
			  && response.StatusCode != HttpStatusCode.NoContent))
			{
				if (response.StatusCode == HttpStatusCode.Unauthorized)
					throw new UnauthorizedAccessException(response.Content);
				else
					throw new RestException(string.Format("status code: {0} response status: {1} message: {2} content: {3}", response.StatusCode, response.ResponseStatus, response.ErrorMessage, response.Content), response.StatusCode);
			}
		}

		#endregion

		public IList<EntityResource> GetEntityModelVendors()
		{
			var request = new RestRequest { Resource = "vendors" };
			var response = Execute<List<Transport.EntityModel.EntityResource>>(request);

			return response.Data;
		}
	}

	[Serializable]
	public class RestException : Exception
	{
		public System.Net.HttpStatusCode HttpErrorCode { get; private set; }

		public RestException(string errorMessage, System.Net.HttpStatusCode statusCode)
			: base(errorMessage)
		{
			this.HttpErrorCode = statusCode;
		}
	}
}
