using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderDAL;
using FloorOrderModels;
using FloorOrderModels.Interfaces;
using FloorOrderModels.Responses;

namespace FloorOrderBLL.Manager
{
    public class TaxStateManager
    {
        private ITaxRepository _taxRepository;

        public TaxStateManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public List<TaxState> GetTaxStates()
        {
            List<TaxState> states = new List<TaxState>();
            states = _taxRepository.LoadAll();
            return states;
        }

        public TaxStateLookupResponse FindTaxState(string stateInput)
        {
            TaxStateLookupResponse response = new TaxStateLookupResponse();

            response.TaxState = _taxRepository.LoadOne(stateInput);

            if (response.TaxState == null)
            {
                response.Success = false;
                response.Message = $"{stateInput} does not exist in our directory";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
