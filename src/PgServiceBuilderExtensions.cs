﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using ArgentSea;
using ArgentSea.Pg;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class adds the injectable PostgreSQL data services into the services collection.
    /// </summary>
	public static class PgServiceBuilderExtensions
	{
        /// <summary>
		/// Loads configuration into injectable Options and the DbDataStores and ShardDataStores services. ILogger service should have already be created.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddPgServices(
			this IServiceCollection services,
			IConfiguration config
			)
		{
            PgServiceBuilderExtensions.AddPgServices<short>(services, config);
            return services;
		}

		/// <summary>
		/// Loads configuration into injectable Options and the DbDataStores and ShardDataStores services. ILogger service should have already be created.
		/// </summary>
		/// <typeparam name="TShard"></typeparam>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddPgServices<TShard>(
			this IServiceCollection services,
			IConfiguration config
			) where TShard : IComparable
		{
            services.Configure<DataResilienceOptions>(config);
            services.Configure<DataSecurityOptions>(config);
            services.Configure<PgDbConnectionOptions>(config);
            services.AddSingleton<PgDatabases>();

            services.Configure<PgShardConnectionOptions<TShard>>(config);
			services.AddSingleton<ShardSetsBase<TShard, PgShardConnectionOptions<TShard>>>();
			return services;
		}
	}
}
