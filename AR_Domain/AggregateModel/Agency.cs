using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Domain.AggregateModel
{
    public class Agency
    {
        private List<Agency> listOfAgency;
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ServiceType { get; private set; }
        public string Location { get; private set; }

        public Agency()
        {
            listOfAgency = new List<Agency>();
            listOfAgency.Add(new Agency() { Code = "0001", Name = "HDB", Description = "This is HDB", ServiceType = "Consulting", Location = "Tuas" });
            listOfAgency.Add(new Agency() { Code = "0002", Name = "HTX", Description = "This is HTX", ServiceType = "Consulting", Location = "Changi" });
        }
        public List<Agency> GetAllAgency()
        {
            return listOfAgency;
        }
        public Agency GetAgencyByCode(string code)
        {
            return listOfAgency.Where(x => x.Code == code).FirstOrDefault();
        }
    }
}
