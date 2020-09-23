using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Quick.Core
{
    public class WorkManager
    {
        public static TransactionScope BeginTransaction()
        {
            return BeginTransaction(TransactionScopeOption.RequiresNew);
        }

        public static TransactionScope BeginTransaction(TransactionScopeOption scopeOption)
        {
            return BeginTransaction(scopeOption, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
        }

        public static TransactionScope BeginTransaction(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
        {
            return new TransactionScope(scopeOption, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {

        }
    }
}
