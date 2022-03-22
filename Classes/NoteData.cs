using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Note_Keeper
{
    public class NoteData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public DateTime DateAdded { set; get; }
        public DateTime DateModified { set; get; }
    }
}
