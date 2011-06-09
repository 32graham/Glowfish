using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glowfish {
    public class Client : IComparable<Client> {
        
        // private members
        private String     firstName;
        private String     lastName;
        private int        numLevel1Minutes;
        private int        numLevel2Minutes;
        private DateTime   level1TanThroughDate;
        private DateTime   level2TanThroughDate;
        private HistoryLog history;

        public  String     FirstName {
            get {
                return firstName;
            }
            set {
                if(value.Trim().Length <= 15) {
                    String name = value.Trim();
                    name = name.ToUpper();
                    firstName = value;
                }
                else {
                    throw new ArgumentException();
                }
            }
        }
        public  String     LastName {
            get {
                return lastName;
            }
            set {
                if(value.Trim().Length <= 15) {
                    String name = value.Trim();
                    name = name.ToUpper();
                    lastName = value;
                }
                else {
                    throw new ArgumentException();
                }
            }
        }
        public  String     Name {
            get {
                return firstName + ' ' + lastName;
            }
        }
        public  int        NumLevel1Minutes {
            get {
                return numLevel1Minutes;
            }
            set {
                if(value >= 0) {
                    numLevel1Minutes = value;
                }
                else {
                    throw new ArgumentException("The number of minutes must be 0 or more.");
                }
            }
        }
        public  int        NumLevel2Minutes {
            get {
                return numLevel2Minutes;
            }
            set {
                if(value >= 0) {
                    numLevel2Minutes = value;
                }
                else {
                    throw new ArgumentException("The number of minutes must be 0 or more.");
                }
            }
        }
        public  DateTime   Level1TanThroughDate {
            get {
                return level1TanThroughDate;
            }
            set {
                level1TanThroughDate = value;
            }
        }
        public  DateTime   Level2TanThroughDate { 
            get {
                return level2TanThroughDate;
            }
            set {
                level2TanThroughDate = value;
            }
        }
        public  bool       HasLevel1Time {
            get {
                bool answer = false;
                if(level1TanThroughDate >= DateTime.Today || numLevel1Minutes > 0) {
                    answer = true;
                }
                return answer;
            }
        }
        public  bool       HasLevel2Time {
            get {
                bool answer = false;
                if(level2TanThroughDate >= DateTime.Today || numLevel2Minutes > 0) {
                    answer = true;
                }
                return answer;
            }
        }
        public  bool       HasLevel1Unlimited {
            get {
                bool answer = false;
                if(level1TanThroughDate >= DateTime.Today) {
                    answer = true;
                }
                return answer;
            }
        }
        public  bool       HasLevel2Unlimited {
            get {
                bool answer = false;
                if(level2TanThroughDate >= DateTime.Today) {
                    answer = true;
                }
                return answer;
            }
        }
        public  HistoryLog History {
            get {
                return history;
            }
            set {
                history = value;
            }
        }
        
        // methods
        public Client() { }
        public Client(String first, String last, int numLv1Mins, int numLv2Mins, DateTime lv1TanThroughDate, DateTime lv2TanThroughDate) {
            firstName = first;
            lastName = last;
            numLevel1Minutes = numLv1Mins;
            numLevel2Minutes = numLv2Mins;
            level1TanThroughDate = lv1TanThroughDate;
            level2TanThroughDate = lv2TanThroughDate;
            history = new HistoryLog();
        }
        public Client(String first, String last) {
            //log = new Log();
            firstName  = first.Trim();
            lastName   = last.Trim();
            numLevel1Minutes = 0;
            numLevel2Minutes = 0;
            level1TanThroughDate = DateTime.Today.AddDays(-1);
            level2TanThroughDate = DateTime.Today.AddDays(-1);
            history = new HistoryLog();
        }
        public override string ToString() {
            return lastName + ", " + firstName;
        }
        public int CompareTo(Client other) {
            return this.ToString().CompareTo(other.ToString());
        }
        public bool Equals(Client other) {
            bool answer = false;
            if(this.firstName == other.firstName && this.lastName == other.lastName) {
                answer = true;
            }
            return answer;
        }
    }
}