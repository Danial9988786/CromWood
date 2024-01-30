﻿using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class StatementTransaction: DBTable
    {
        public Guid Id { get; set; }
        public string PaidBy { get; set; } // It can be housing benefit or Tenant.
        public Guid? PaidByTenantId { get; set; }
        public Tenant? PaidByTenant { get; set; }
        public Guid? StatementId { get; set; }
        public TenancyStatement Statement { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid TransactionModeId { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDescription { get; set; }
    }
}
