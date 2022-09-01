using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace Note_Keeper
{
    class DataAccess
    {
        public static event EventHandler OnDeleted;

        internal static void Delete(NoteData data)
        {
            File.Delete(Utils.GetIdPath(data.Id));

            OnDeleted?.Invoke(null,null);
        }

        internal static NoteData[] ReadNotesList()
        {
            List<NoteData> noteList = new List<NoteData>();
            DirectoryInfo info = new DirectoryInfo("data");

            foreach (var file in info.GetFiles())
            {
                NoteData note = new NoteData(file);
                if(note.Id + "" == file.Name)
                    noteList.Add(note);
            }

            noteList.Sort((x, y) => DateTime.Compare(y.DateModified, x.DateModified));

            return noteList.ToArray();
        }
    }
}
