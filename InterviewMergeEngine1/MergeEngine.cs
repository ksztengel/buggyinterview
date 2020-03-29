using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CongaInterview
{
    class MergeEngine
    {
        string _inputData;
        string _outPath;
        List<Account> _accounts;
        List<Opportunity> _opportunities;
        List<Contact> _contacts;


        private class Account
        {
            public string ID;
            public string Name;
            public string Contact;
            public string BillingStreetAddress;
            public string BillingCity;
            public string BillingState;
            public string BillingZipCode;
        }

        private class Opportunity
        {
            public string Name;
            public string AccountID;
            public float ProbabilityPercent;
            public float Amount;
        }

        private class Contact
        {
            public string ID;
            public string Name;
            public string PhoneNumber;
        }


        public MergeEngine(string inputDataFullPath, string outputFullPath)
        {
            _inputData = inputDataFullPath;
            _outPath = outputFullPath;
            _accounts = new List<Account>();
            _contacts = new List<Contact>();
            _opportunities = new List<Opportunity>();
        }

        public void Run()
        {
            ParseInputData();
            ReportByOpportunityAmount();
        }

        private void ReportByOpportunityAmount()
        {
            //creates a csv report of the form:
            /*OppName   OppAmount   OppProbability  AccountName  */
            SortOpportunities();
            var builder = new StringBuilder();
            builder.Append("OppName,OppAmount,OppProbability,AccountName\n");
            foreach(var o in _opportunities)
            {
                var acctName = LookupAccount(o.AccountID);
                builder.Append(o.Name + "," +
                    o.Amount + "," +
                    o.ProbabilityPercent + "," +
                    acctName + "\n");
            }
            File.AppendAllText(_outPath, builder.ToString());
        }

        private string LookupAccount(string acctID)
        {
            foreach(var a in _accounts)
            {
                if(a.ID == acctID)
                {
                    return a.Name;
                }
            }
            return "";
        }

        private void SortOpportunities()
        {
            //creates a new sorted list of opportunities
            var numOpportunities = _opportunities.Count;
            var sortedList = new List<Opportunity>();
            for (int i = 0; i < numOpportunities; i++)
            {
                var maxOppty = GetLargestOpportunity();
                _opportunities.Remove(maxOppty);
                sortedList.Add(maxOppty);
            }

            _opportunities = sortedList;
        }

        private Opportunity GetLargestOpportunity()
        {
            float maxAmount = 0.0f;
            var maxOpportunity = new Opportunity();
            foreach(var o in _opportunities)
            {
                if (o.Amount > maxAmount)
                {
                    maxAmount = o.Amount;
                    maxOpportunity = o;
                }
            }
            return maxOpportunity;
        }

        /* internals */
        private void ParseInputData()
        {
            var data = File.ReadAllText(_inputData);
            var blocks = data.Split("\r\n\r\n");
            foreach (var b in blocks)
            {
                var blockType = b.Split("\r\n")[0];
                switch (blockType)
                {
                    case "ACCOUNT":
                        ParseAccount(b);
                        break;
                    case "OPPORTUNITY":
                        ParseOpportunity(b);
                        break;
                    case "CONTACT":
                        ParseContact(b);
                        break;
                }
            }
        }

        private void ParseAccount(string block)
        {
            var insides = GetInsidesOfBlock(block);
            var a = new Account
            {
                ID = GetValue(insides[1]),
                Name = GetValue(insides[2]),
                Contact = GetValue(insides[3]),
                BillingStreetAddress = GetValue(insides[4]),
                BillingCity = GetValue(insides[5]),
                BillingState = GetValue(insides[6]),
                BillingZipCode = GetValue(insides[7])
            };

            _accounts.Add(a);

        }


        private void ParseContact(string block)
        {
            var insides = GetInsidesOfBlock(block);
            var c = new Contact
            {
                ID = GetValue(insides[1]),
                Name = GetValue(insides[2]),
                PhoneNumber = GetValue(insides[3])
            };
            _contacts.Add(c);
        }

        private void ParseOpportunity(string block)
        {
            var insides = GetInsidesOfBlock(block);
            var o = new Opportunity
            {
                Name = GetValue(insides[1]),
                AccountID = GetValue(insides[2]),
                ProbabilityPercent = float.Parse(GetValue(insides[3])),
                Amount = float.Parse(GetValue(insides[4]))
            };
            _opportunities.Add(o);
        }

        private static string[] GetInsidesOfBlock(string block)
        {
            var insideMinusHeader = block.Split("{")[1];
            return insideMinusHeader.Split("\t");
        }

        private static string GetValue(string line)
        {
            var valPlusJunk = line.Split(':')[1];
            return valPlusJunk.Split('\r')[0];
        }

    }
}
