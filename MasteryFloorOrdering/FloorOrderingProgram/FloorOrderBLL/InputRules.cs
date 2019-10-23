using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels.Responses;

namespace FloorOrderBLL
{
    public class InputRules : Response
    {
        public DateCheckResponse DateCheck(DateTime orderDate)
        {
            DateCheckResponse response = new DateCheckResponse();

            if(orderDate < DateTime.Today)
            {
                response.Success = false;
                response.Message = "Order Dates must not be a past date.  Please try again:";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public NameCheckResponse NameCheck(string nameInput)
        {
            NameCheckResponse response = new NameCheckResponse();
            string allowed = "qwertyuioplkjhgfdsazxcvbnm QWERTYUIOPLKJHGFDSAZXCVBNM1234567890,.";
            foreach (var letter in nameInput)
            {
                if (!allowed.Contains(letter))
                {
                    response.Success = false;
                    response.Message = "Name input contains unallowed characters";
                    return response;
                }

            }
            if (nameInput.Trim() == "")
            {
                response.Success = false;
                response.Message = "Try again, and please enter at least something";
                return response;
            }
            else
            {
                response.Success = true;
                return response;
            }
        }

        public StateInputCheckResponse StateCheck(string stateInput)
        {
            StateInputCheckResponse response = new StateInputCheckResponse();

            if(stateInput == "")
            {
                response.Success = false;
                response.Message = "Try again, and please enter at least something";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public ProductInputCheckResponse ProductCheck(string productInput)
        {
            ProductInputCheckResponse response = new ProductInputCheckResponse();

            if(productInput == "")
            {
                response.Success = false;
                response.Message = "Try again, and please enter at least something";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public AreaCheckResponse AreaCheck(decimal areaInput)
        {
            AreaCheckResponse response = new AreaCheckResponse();
            
            if(areaInput < 100)
            {
                response.Success = false;
                response.Message = "We do not accept orders with an Area of less than 100";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
