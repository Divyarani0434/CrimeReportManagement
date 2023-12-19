using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Entities { 
    internal class Officers
    {
        int officerID;
        string firstName;
        string lastName;
        int badgeNumber;
        string rank;
        string contactInformation;
        int agencyID;
        public int OfficerID {  get { return officerID; } set {  officerID = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public int BadgeNumber { get {  return badgeNumber; } set {  badgeNumber = value; } }
        public string Rank {  get { return rank; } set {  rank = value; } }
        public string ContactInformation { get {  return contactInformation; } set {  contactInformation = value; } }
        public int AgencyID { get { return agencyID; } set {  agencyID = value; } }
        public Officers(int officerID,string firstName,string lastName,int badgeNumber,string rank,string contactInformation,int agencyID) {
            OfficerID = officerID;
            FirstName = firstName;
            LastName = lastName;
            BadgeNumber = badgeNumber;
            Rank = rank;
            ContactInformation = contactInformation;
            AgencyID = agencyID;
        }
    }
}
