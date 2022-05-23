using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Note_Keeper
{
    class DbManager
    {
        #region Events

        public static event EventHandler OnDeleted;

        #endregion

        #region Db Connection

        public static SqlConnection connection;
        static DbManager()
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + assembly.GetName().Name + "\\Notes.mdf");

                if (!fileInfo.Exists)
                {
                    if (!fileInfo.Directory.Exists)
                        fileInfo.Directory.Create();

                    using (var res = assembly.GetManifestResourceStream("Note_Keeper.Notes.mdf"))
                    {
                        FileStream dbFile = fileInfo.Open(FileMode.Create, FileAccess.Write);
                        res.CopyTo(dbFile);
                        dbFile.Close();
                    }
                }

                connection = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={fileInfo.FullName};Integrated Security=True;Connect Timeout=30");


            }
            catch (Exception ex)
            {
                MessageBox.Show("We can't save your data an error occered when create database : \r\n\r\n" + ex);
                Environment.Exit(1);
            }
        }

        #endregion

        #region CRUD Functions

        internal static int Add(string title,string content)
        {
            Execute(

                "AddNote @Title,@Content", 
                
                new IDbDataParameter[] {
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Content", content) });

            return GetLastId();
        }

        internal static void Update(int id,string title,string content)
        {
            Execute(

                "EditNote @Id,@Title,@Content",

                new IDbDataParameter[] {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Content", content) });
        }
        
        internal static void Delete(int id)
        {
            Execute("DeleteNote @Id", new IDbDataParameter[] { new SqlParameter("@Id", id) });
            OnDeleted?.Invoke(null,null);
        }

        internal static NoteData[] ReadNotesList()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var list = ExecuteReader<NoteData>("ReadNoteList");

            sw.Stop();
            File.AppendAllText("G:\\notekeeper.txt",$"{DateTime.Now}: Reading note list time {sw.ElapsedMilliseconds}ms \r\n");
            return list;
        }

        internal static NoteData ReadNote(int id)
        {
            return ExecuteReader<NoteData>("ReadNote @Id",new IDbDataParameter[] { new SqlParameter("@Id",id)})[0];
        }

        internal static int GetLastId()
        {
            OpenConnection();

            var command = connection.CreateCommand();
            command.CommandText = "GetLastID";

            object firstRecordField = command.ExecuteScalar();

            return firstRecordField is int ? (int)firstRecordField : 0;

        }

        #endregion


        public static T[] ExecuteReader<T>(string commandText, IDbDataParameter[] parameters = null)
        {

            OpenConnection();

            List<T> lstNotes = new List<T>();

            var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
                foreach (var param in parameters)
                    command.Parameters.Add(param);


            var reader = command.ExecuteReader();

            var propertyList = typeof(T).GetProperties();

            while (reader.Read())
            {
                object data = Activator.CreateInstance(typeof(T));

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    foreach (var property in propertyList)
                    {
                        if (property.Name == reader.GetName(i))
                        {
                            property.SetValue(data, reader.GetValue(i), null);
                            break;
                        }
                    }
                }

                lstNotes.Add((T)data);
            }


            return lstNotes.ToArray();

        }

        static void Execute(string commandText, IDbDataParameter[] parameters)
        {
            OpenConnection();

            var command = connection.CreateCommand();
            command.CommandText = commandText;

            foreach (var param in parameters)
                command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        static void OpenConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            connection.Open();
        }
    }
}
