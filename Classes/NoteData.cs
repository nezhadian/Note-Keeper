using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Note_Keeper
{
    public class NoteData
    {
        private FileInfo _document;
        public FileInfo Document
        {
            get { return _document; }
            set { 
                _document = value;
                Title = value.Name;
                Content = new StreamReader(value.OpenRead()).ReadToEnd();
                DateAdded = value.CreationTime;
                DateModified = value.LastWriteTime;
            }
        }


        public int Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public DateTime DateAdded { set; get; }
        public DateTime DateModified { set; get; }
        public NoteData()
        {

        }
        public NoteData(FileInfo document)
        {
            Document = document;
        }
    }
}
