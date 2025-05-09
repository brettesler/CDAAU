using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamUnicorn.CDAAU.Core 
{

    public class identifier
    {
        public string Root { get; set; }
        public string Extension { get; set; }
        public string AuthorityName { get; set; }
    }

    public class codeable
    {
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string DisplayName { get; set; }
        public string CodeSystemName { get; set; }
        public string OriginalText { get; set; }
        public string NullFlavor { get; set; }
    }

    public class physical_quantity
    {
        public float Value { get; set; }
        public string Unit { get; set; }
    }

    public class time_period
    {
        public time_period()
        {
            FromInclusive = true;
            ToInclusive = false;
        }

        public DateTime? From { get; set; }
        public bool FromInclusive { get; set; }
        public DateTime? To;
        public bool ToInclusive { get; set; }
    }
    
    public class quantity_range
    {
        public quantity_range()
        {
            LowInclusive = true;
        }

        public physical_quantity High { get; set; }
        public bool HighInclusive { get; set; }
        public physical_quantity Low;
        public bool LowInclusive { get; set; }
    }

    public class name
    {
        public string Family { get; set; }
        public string Given1 { get; set; }
        public string Given2 { get; set; }
        public string Title { get; set; }
    }

    public class address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }

    public class employer
    {
        public codeable JobRole { get; set; }
        public string HPIO { get; set; }
        public string DepartmentName { get; set; }
        public string OrganizationName { get; set; }

        public address Address { get; set; }
        public string WorkPhone { get; set; }
        public string WorkFax { get; set; }
        public string WorkEmail { get; set; }
    }
}
