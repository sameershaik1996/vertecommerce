using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerteCommerce.Services.Cart.Core.Domain.Cart;

namespace VerteCommerce.Services.Cart.Data
{
	using Core.Domain.Cart;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using StackExchange.Redis;

	public class RedisCartRepository : IRepository
	{
		#region properties

		private readonly ILogger<RedisCartRepository> _logger;
		private readonly ConnectionMultiplexer _redis;//object to connect to redis database
		private readonly IDatabase _database;

		#endregion

		#region ctors

		public RedisCartRepository(	ConnectionMultiplexer redis,
									ILoggerFactory loggerFactory
								  )
		{
			_logger = loggerFactory.CreateLogger<RedisCartRepository>();
			_redis = redis;
			_database = redis.GetDatabase();
		}

		#endregion

		#region methods
		public bool DeleteCart(string id)
		{
			return _database.KeyDelete(id);
		}

		public Cart GetCart(string cartId)
		{
			var cart = _database.StringGet(cartId);
			if(cart.IsNullOrEmpty)
			{
				return null;
			}
			return JsonConvert.DeserializeObject<Cart>(cart);
		}

		public IEnumerable<string> GetUsers()
		{
			var server = GetServer();
			var data = server.Keys();
			return data?.Select(k => k.ToString());
		}

		public Cart UpdateCart(Cart basket)
		{
			var cart = _database.StringSet(basket.BuyerId, JsonConvert.SerializeObject(basket));
			if (!cart)
			{
				_logger.LogInformation("Problem occur persisting the item.");
				return null;
			}

			_logger.LogInformation("Basket item persisted succesfully.");

			return GetCart(basket.BuyerId);
		}

		private IServer GetServer()
		{
			var endpoint = _redis.GetEndPoints();
			return _redis.GetServer(endpoint.First());
		}

		#endregion
	}
}
