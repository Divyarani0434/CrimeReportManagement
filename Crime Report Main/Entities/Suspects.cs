using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Entities
{
    internal class Suspects
    {
        int suspectID;
        string firstName;
        string lastName;
        string dateOfBirth;
        string gender;
        string contactInformation;
        public int SuspectID {  get { return suspectID; } set { suspectID = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string DateOfBirth { get {  return dateOfBirth; } set {  dateOfBirth = value; } }
        public string Gender { get {  return gender; } set {  gender = value; } }
        public string ContactInformation { get {  return contactInformation; } set {  contactInformation = value; } }
        public Suspects(int suspectID,string firstName,string lastName,string dateOfBirth,string gender,string contactInformation) {
            SuspectID = suspectID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ContactInformation = contactInformation;

        }
    }
}
