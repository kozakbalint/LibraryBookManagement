using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBookManagementApp
{
    class Rent
    {
        private int id;
        private int memberId;
        private int bookId;
        private DateTime rentDate;
        private DateTime rentEndDate;

        public int RentId
        {
            get { return id; }
            set { id = value; }
        }

        public int RentMemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public int RentBookId
        {
            get { return bookId; }
            set { bookId = value; }
        }

        public DateTime RentDate
        {
            get { return rentDate; }
            set { rentDate = value; }
        }

        public DateTime RentEndDate
        {
            get { return rentEndDate; }
            set { rentEndDate = value; }
        }

        public Rent(string record)
        {
            string[] splittedRecord = record.Split(';');
            this.id = int.Parse(splittedRecord[0]);
            this.memberId = int.Parse(splittedRecord[1]);
            this.bookId = int.Parse(splittedRecord[2]);
            this.rentDate = DateTime.Parse(splittedRecord[3]);
            this.rentEndDate = DateTime.Parse(splittedRecord[4]);
        }

        public Rent(int id, int memberId, int bookId, DateTime rentDate, int rentDuration)
        {
            this.id = id;
            this.memberId = memberId;
            this.bookId = bookId;
            this.rentDate = rentDate;
            this.rentEndDate = rentDate;
            this.rentEndDate = rentEndDate.AddDays(rentDuration);
        }
    }
}
