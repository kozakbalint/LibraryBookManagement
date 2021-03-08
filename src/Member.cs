using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBookManagementApp
{
    class Member
    {
        private int id;
        private string name;
        private string birthDate;
        private int zip;
        private string city;
        private string street;

        public int MemberId
        {
            get { return id; }
            set { id = value; }
        }
        public string MemberName
        {
            get { return name; }
            set { name = value; }
        }
        public string MemberBirth
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        public int MemberZip
        {
            get { return zip; }
            set { zip = value; }
        }
        public string MemberCity
        {
            get { return city; }
            set { city = value; }
        }
        public string MemberStreet
        {
            get { return street; }
            set { street = value; }
        }

        public Member(string record)
        {
            string[] splittedRecord = record.Split(';');
            id = int.Parse(splittedRecord[0]);
            name = splittedRecord[1];
            birthDate = splittedRecord[2];
            zip = int.Parse(splittedRecord[3]);
            city = splittedRecord[4];
            street = splittedRecord[5];
        }

        public Member(int id, string name, string birthDate, int zip, string city, string street)
        {
            this.id = id;
            this.name = name;
            this.birthDate = birthDate;
            this.zip = zip;
            this.city = city;
            this.street = street;
        }
    }
}
