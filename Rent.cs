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
    }
}
