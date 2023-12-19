using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Entities
{
    internal class LawEnforcementAgencies
    {
        int agencyID;
        string agencyName;
        string jurisdiction;
        string contactInformation;
        public int AgencyID { get { return agencyID; } set {  agencyID = value; } }
        public string AgencyName { get {  return agencyName; } set { agencyName = value; } }
        public string Jurisdiction { get {  return jurisdiction; } set {  jurisdiction = value; } }
        public string ContactInformation { get {  return contactInformation; } set {  contactInformation = value; } }
        public LawEnforcementAgencies(int agencyID,string agencyName,string jurisdiction,string contactInformation) {
            AgencyID = agencyID;
            AgencyName = agencyName;
            Jurisdiction = jurisdiction;
            ContactInformation = contactInformation;
        }
    }
}
