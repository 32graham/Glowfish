using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glowfish {
    public class HistoryLog {

        //private List<String> data;
        private Queue<String> data;
        private int length = 30;

        public HistoryLog() {
            data = new Queue<String>();
        }

        public void Tanned(int numMinutes, int level) {
            string note = "Tanned " + numMinutes + " minutes on " + DateTime.Now.ToString() + " at level " + level;
            Add(note);
        }

        public void Edited() {
            string note = "Edited on " + DateTime.Now.ToString();
            Add(note);
        }

        public String ToScreen() {
            
            string val = "";
            foreach(string s in data.Reverse()) {
                if(s == "") { continue; }
                val += s + '\n';
            }
            return val;

        }

        public String ToDatabase() {
            string val = "";
            foreach(string s in data) {
                if(s == "") { continue; }
                val += s + '\n';
            }
            return val;
        }

        private void Add(string entry) {
            

            while(data.Count >= length) {
                data.Dequeue();
            }

            data.Enqueue(entry.Trim());
        }

        internal void DataManagerAdd(string entry) {
            Add(entry);
        }
    }
}
