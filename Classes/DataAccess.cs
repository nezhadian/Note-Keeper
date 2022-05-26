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
        #region Events

        public static event EventHandler OnDeleted;

        #endregion

        #region Init

        static DataAccess()
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");
        }

        #endregion

        #region CRUD Functions

        internal static int Add(NoteData data)
        {

            int id = GetLastId() + 1;
            data.Id = id;
            data.DateAdded = data.DateModified = DateTime.Now;

            SaveNote(data);
            return id;
            
        }

        internal static void Update(NoteData data)
        {
            data.DateModified = DateTime.Now;
            SaveNote(data);
        }

        private static void SaveNote(NoteData data)
        {
            FileStream fs = File.OpenWrite(GetIdPath(data.Id));
            XmlSerializer xml = new XmlSerializer(typeof(NoteData));
            
            xml.Serialize(fs, data);
            fs.SetLength(fs.Position);
            fs.Flush();
            fs.Close();
        }

        internal static void Delete(int id)
        {
            File.Delete(GetIdPath(id));

            OnDeleted?.Invoke(null,null);
        }

        internal static NoteData[] ReadNotesList()
        {
            List<NoteData> noteList = new List<NoteData>();
            XmlSerializer xml = new XmlSerializer(typeof(NoteData));

            DirectoryInfo info = new DirectoryInfo("data");
            foreach (var file in info.GetFiles())
            {
                NoteData note = (NoteData)xml.Deserialize(file.OpenRead());
                if(note.Id + "" == file.Name)
                    noteList.Add(note);
            }

            return noteList.ToArray();
        }

        internal static int GetLastId()
        {
            int max = 0;
            foreach (string file in Directory.GetFiles("data"))
            {
                int.TryParse(Path.GetFileName(file), out int i);
                max = Math.Max(i, max);
            }
            return max;
        }

        private static string GetIdPath(int id)
        {
            return Path.Combine("data", id.ToString());
        }
        

        #endregion
    }
}
