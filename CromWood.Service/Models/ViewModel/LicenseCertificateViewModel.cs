﻿using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class LicenseCertificateViewModel
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public PropertyViewModel Property { get; set; }
        public Guid LicenseCertificateTypeId { get; set; }
        public LicenseCertificateType LicenseCertificateType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DocUrl { get; set; }
        public string Note { get; set; }
        public bool? Archieved { get; set; }
        public Guid? CreatedBy { get; set; }

    }
}
