using System;                     // For system functions like Console.
using System.Collections.Generic; // For generic collections like List.
using System.Data.SqlClient;      // For the database connections and objects.
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Glowfish {
    public class DataManager {

        public static void TryCreateTable() {
            using(SqlConnection con = new SqlConnection(
            Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                con.Open();
                try {
                    using(SqlCommand command = new SqlCommand(
                        "CREATE TABLE Clients (FirstName NVARCHAR(40), LastName NVARCHAR(40), NumLevelOneMinutes INT, NumLevelTwoMinutes INT, LevelOneTanThroughDate DATETIME, LevelTwoTanThroughDate DATETIME, History NVARCHAR(2000))", con)) {
                        command.ExecuteNonQuery();
                    }
                    
                    AddClient(new Client("Rachel", "Mosely", 100, 100, DateTime.Now, DateTime.Now));
                    AddClient(new Client("Brandy", "Graham", 200, 200, DateTime.Now, DateTime.Now));
                    AddClient(new Client("Josh",   "Graham", 300, 300, DateTime.Now, DateTime.Now));
                    AddClient(new Client("Candi",  "Ksiazek", 400, 400, DateTime.Now, DateTime.Now));

                    MessageBox.Show("Table Created.");
                }
                catch {
                    MessageBox.Show("Table could not be created. It likely already exist.");
                }
            }
        }

        public static void AddClient(Client c) {
            using(SqlConnection con = new SqlConnection(Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                con.Open();
                try {
                    using(SqlCommand command = new SqlCommand("INSERT INTO Clients VALUES(@FirstName, @LastName, @NumLevelOneMinutes, @NumLevelTwoMinutes, @LevelOneTanThroughDate, @LevelTwoTanThroughDate, @History)", con)) {
                        command.Parameters.Add(new SqlParameter("FirstName", c.FirstName));
                        command.Parameters.Add(new SqlParameter("LastName", c.LastName));
                        command.Parameters.Add(new SqlParameter("NumLevelOneMinutes", c.NumLevel1Minutes));
                        command.Parameters.Add(new SqlParameter("NumLevelTwoMinutes", c.NumLevel2Minutes));
                        command.Parameters.Add(new SqlParameter("LevelOneTanThroughDate", c.Level1TanThroughDate));
                        command.Parameters.Add(new SqlParameter("LevelTwoTanThroughDate", c.Level2TanThroughDate));
                        command.Parameters.Add(new SqlParameter("History", c.History.ToDatabase()));
                        command.ExecuteNonQuery();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show("error in AddClient\n" + e.Message);
                }
            }
        }

        public static void ParseName(string fullName, out string firstName, out string lastName) {
            string[] names = fullName.Split(',');
            lastName = names[0].Trim();
            firstName = names[1].Trim();
        }

        private static HistoryLog ParseHistoryLog(string hist) {
            
            HistoryLog histLog = new HistoryLog();
            List<string> entries = new List<string>();
            
            foreach(string s in hist.Split('\n')) {
                entries.Add(s);
            }

            foreach(string s in entries) {
                histLog.DataManagerAdd(s);
            }

            return histLog;
        }

        public static Client GetClient(string fullName) {
            string firstName;
            string lastName;

            ParseName(fullName, out firstName, out lastName);

            return GetClient(firstName, lastName);
        }

        public static Client GetClient(string firstName, string lastName) {
            
            Client c = new Client();

            using(SqlConnection con = new SqlConnection(Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                con.Open();
                try {
                    using(SqlCommand command = new SqlCommand("SELECT * FROM Clients WHERE FirstName = @firstName AND LastName = @lastName", con)) {
                        command.Parameters.Add(new SqlParameter("firstName", firstName));
                        command.Parameters.Add(new SqlParameter("lastName", lastName));
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();
                        
                        c.FirstName        = reader.GetString(0);
                        c.LastName         = reader.GetString(1);
                        c.NumLevel1Minutes = reader.GetInt32(2);
                        c.NumLevel2Minutes = reader.GetInt32(3);
                        c.Level1TanThroughDate = reader.GetDateTime(4);
                        c.Level2TanThroughDate = reader.GetDateTime(5);
                        string hist = reader.GetString(6);
                        c.History = ParseHistoryLog(hist);
                    }
                }
                catch(Exception e) {
                    MessageBox.Show("error in GetClient(string, string)\n" + e.Message);
                }
                return c;
            }
        }

        public static void RemoveClient(string name) {
            string first;
            string last;

            ParseName(name, out first, out last);

            RemoveClient(first, last);
        }

        public static void RemoveClient(string firstName, string lastName) {

            using(SqlConnection con = new SqlConnection(Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                con.Open();
                try {
                    using(SqlCommand command = new SqlCommand("DELETE FROM Clients WHERE FirstName = @firstName AND LastName = @lastName", con)) {
                        command.Parameters.Add(new SqlParameter("firstName", firstName));
                        command.Parameters.Add(new SqlParameter("lastName", lastName));

                        command.ExecuteNonQuery();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show("error in RemoveClient(string, string)\n" + e.Message);
                }
            }
        }

        public static void UpdateClient(Client c) {

            using(SqlConnection con = new SqlConnection(Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                con.Open();
                try {
                    using(SqlCommand command = new SqlCommand("UPDATE Clients SET NumLevelOneMinutes = @NumLevel1Minutes, NumLevelTwoMinutes = @NumLevel2Minutes, LevelOneTanThroughDate = @Level1TanThroughDate, LevelTwoTanThroughDate = @Level2TanThroughDate, History = @History WHERE FirstName = @FirstName AND LastName = @LastName", con)) {
                        command.Parameters.Add(new SqlParameter("NumLevel1Minutes", c.NumLevel1Minutes));
                        command.Parameters.Add(new SqlParameter("NumLevel2Minutes", c.NumLevel2Minutes));
                        command.Parameters.Add(new SqlParameter("Level1TanThroughDate", c.Level1TanThroughDate));
                        command.Parameters.Add(new SqlParameter("Level2TanThroughDate", c.Level2TanThroughDate));
                        command.Parameters.Add(new SqlParameter("History", c.History.ToDatabase()));
                        command.Parameters.Add(new SqlParameter("FirstName", c.FirstName));
                        command.Parameters.Add(new SqlParameter("LastName", c.LastName));

                        command.ExecuteNonQuery();
                    }
                }
                catch(Exception e) {
                    MessageBox.Show("Error in UpdateClient(Client)\n" + e.Message);
                }
            }
        }


        public static List<string> NameList {
            get {
                List<string> names = new List<string>();
                using(SqlConnection con = new SqlConnection(
                Glowfish.Properties.Settings.Default.GlowfishDataConnectionString)) {
                    con.Open();

                    using(SqlCommand command = new SqlCommand("SELECT FirstName, LastName FROM Clients", con)) {
                        SqlDataReader reader = command.ExecuteReader();
                        Client c = new Client();
                        while(reader.Read()) {
                            c.FirstName = reader.GetString(0);
                            c.LastName = reader.GetString(1);
                            names.Add(c.ToString());
                        }
                    }
                }
                names.Sort();
                return names;
            }
        }
    }
}

