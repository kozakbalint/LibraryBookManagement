﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryBookManagementApp
{
    class Book
    {
        private int id;
        private string author;
        private string title;
        private string releaseDate;
        private string publisher;
        private bool isRentable;

        public int BookId
        {
            get { return id; }
            set { id = value; }
        }

        public string BookAuthor
        {
            get { return author; }
            set { author = value; }
        }

        public string BookTitle
        {
            get { return title; }
            set { title = value; }
        }

        public string BookReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }

        public string BookPublisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        public bool IsRentable
        {
            get { return isRentable; }
            set { isRentable = value; }
        }

        public Book(string record)
        {
            string[] splittedRecord = record.Split(';');
            id = int.Parse(splittedRecord[0]);
            author = splittedRecord[1];
            author = splittedRecord[2];
            author = splittedRecord[3];
            author = splittedRecord[4];
            isRentable = bool.Parse(splittedRecord[5]);
        }
    }
}
