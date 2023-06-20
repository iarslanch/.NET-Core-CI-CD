﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreForce.Client;
using NetCoreForce.Linq.Conventions.Naming;
using NetCoreForce.Linq.Entity;

namespace NetCoreForce.Linq.Providers
{
    public class ForceClientSalesforceProvider<T> : SalesforceProviderBase<T> where T : class
    {
        public ForceClientSalesforceProvider(ForceClient forceClient, ISalesforceNamingConvention objectNameProvider, SelectTypeEnum selectType = SelectTypeEnum.SelectIdAndUseAttachModel) : base(objectNameProvider, selectType)
        {
            ForceClient = forceClient;
        }

        public ForceClient ForceClient { get; }

        protected override Task<int> ProduceCountAsync(string cmd)
        {
            return ForceClient.CountQuery(cmd, queryAll:false);
        }

        protected override IAsyncEnumerator<T> ProduceAsyncEnumerator(string cmd)
        {
            return this.ForceClient.QueryAsync<T>(cmd, queryAll:false).GetAsyncEnumerator();
        }
    }
}