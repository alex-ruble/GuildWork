using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FloorOrderModels;
using FloorOrderModels.Interfaces;

namespace FloorOrderDAL
{
    public class TaxRepository : ITaxRepository
    {
        public List<TaxState> LoadAll()
        {
            List<TaxState> states = new List<TaxState>();

            using (StreamReader sr = new StreamReader(Settings.TaxFilePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    TaxState state = new TaxState();

                    string[] column = line.Split(',');

                    state.StateCode = column[0];
                    state.StateName = column[1];
                    state.TaxRate = decimal.Parse(column[2]);

                    states.Add(state);
                }
            }
            return states;
        }
        public TaxState LoadOne(string stateName)
        {
            TaxState state = null;

            using (StreamReader sr = new StreamReader(Settings.TaxFilePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] column = line.Split(',');

                    if (column[1] == stateName)
                    {
                        state = new TaxState();

                        state.StateName = column[1];
                        state.StateCode = column[0];
                        state.TaxRate = decimal.Parse(column[2]);
                    }
                }
            }
            return state;
        }
    }
}
