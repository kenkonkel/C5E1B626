﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.EntityManager.DataService.Model;

namespace EntityModelPOC.Model
{
	public static class EntityResourceExtensions
	{
		public static string DisplayName (this EntityResource resource)
		{
			return resource.Name + ": " + resource.Description;
		}
	}
}
