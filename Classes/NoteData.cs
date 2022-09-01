using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Note_Keeper
{
    public class NoteData
    {
        [XmlIgnore]
        public static XmlSerializer XmlSerializer = new XmlSerializer(typeof(NoteData));

        [XmlIgnore]
        private FileInfo _document;

        public int Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }

        [XmlIgnore]
        public DateTime DateAdded => _document.CreationTime;
        [XmlIgnore]
        public DateTime DateModified => _document.LastWriteTime;

        public NoteData(FileInfo document)
        {
            NoteData data = (NoteData)XmlSerializer.Deserialize(document.OpenRead());
            Id = data.Id;
            Title = data.Title;
            Content = data.Content;
            _document = document;

        }
        public NoteData()
        {
            Id = GetLastId() + 1;
        }

        public void Save()
        {
            FileInfo fileInfo = new FileInfo(Utils.GetIdPath(Id));

            Stream fs = fileInfo.Exists ? fileInfo.Open(FileMode.Truncate) : fileInfo.Create();

            XmlSerializer.Serialize(fs,this);

            fs.Close();
        }
        
        internal static int GetLastId()
        {
            int max = 0;
            foreach (string file in Directory.GetFiles("data"))
            {
                if (int.TryParse(Path.GetFileName(file), out int i))
                    max = Math.Max(i, max);

            }
            return max;
        }
    }
}
