using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Runtime;

namespace TenLi.Api.DataAccess.Mongo
{
	public class Entity
	{
		[BsonId]
		[JsonIgnore]
		public virtual ObjectId _id { get; set; }
	}
}
