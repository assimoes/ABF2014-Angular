using ABF2014Model;
using ABFAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ABFAPI.Controllers
{
    public class BillingController : ApiController
    {
        FacturacaoAmbimedEntities _context = new FacturacaoAmbimedEntities();
        // GET api/<controller>
        public Guid GetBillingToken()
        {

            ObjectParameter paramobject = new ObjectParameter("Token", typeof(Guid));
            
            int res =  _context.GenerateExecutionToken("SSIMOES", paramobject);

            return (Guid)paramobject.Value;

        }

        //[Authorize]
        [HttpGet]
        public IResponse DefineCustomers(Guid token,int periodofacturacao,string clientes)
        {
           
            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.FillCustomerUniverse(token, periodofacturacao, clientes, result);


            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1];
            response.Status = int.Parse(r[0]);
            response.Phase = 0;

            return response;
        }

        [HttpGet]
        public IResponse FillCustomerTable(Guid token)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.FillCustomerTempTable(token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1];
            response.Status = int.Parse(r[0]);
            response.Phase = 1;
            return response;
        }

        [HttpGet]
        public IResponse FillContractTable(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.FillContractTempTables(token,periodofacturacao, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1];
            response.Status = int.Parse(r[0]);
            response.Phase = 1;
            return response;
        }

        [HttpGet]
        public IResponse FillAuxTables(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.FillRemainingTempTables(token, periodofacturacao, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1];
            response.Status = int.Parse(r[0]);
            response.Phase = 1;
            return response;
        }

        [HttpGet]
        public IResponse CheckForErrorsPhase1(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.CheckForErrorsPhase1(token, periodofacturacao, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1];
            response.Status = int.Parse(r[0]);
            response.Phase = 3;
            return response;
        }

        [HttpGet]
        public IResponse[] ExecutePhase0(Guid token, int periodofacturacao, string clientes)
        {
            List<IResponse> responses = new List<IResponse>();
            ObjectParameter result = new ObjectParameter("Result", typeof(string));
            IResponse response = new BillingResponse();

            responses.Add(DefineCustomers(token, periodofacturacao,clientes));

            return responses.ToArray();

        }

        [HttpGet]
        public IResponse[] ExecutePhase1(Guid token, int periodofacturacao)
        {
            List<IResponse> responses = new List<IResponse>();
            ObjectParameter result = new ObjectParameter("Result", typeof(string));
            IResponse response = new BillingResponse();

            responses.Add(FillCustomerTable(token));
            responses.Add(FillContractTable(token, periodofacturacao));
            responses.Add(FillAuxTables(token, periodofacturacao));

            return responses.ToArray();

        }

        [HttpGet]
        public IResponse[] ExecutePhase2(Guid token, int periodofacturacao)
        {
            List<IResponse> responses = new List<IResponse>();
            ObjectParameter result = new ObjectParameter("Result", typeof(string));
            IResponse response = new BillingResponse();

            responses.Add(Regra1(token, periodofacturacao));
            responses.Add(Regra2(token, periodofacturacao));
            responses.Add(Regra3(token, periodofacturacao));
            responses.Add(Regra4(token, periodofacturacao));
            responses.Add(Regra5(token, periodofacturacao));
            responses.Add(Regra6(token, periodofacturacao));
            responses.Add(Regra7(token, periodofacturacao));
            responses.Add(Regra8(token, periodofacturacao));
            responses.Add(Regra19(token, periodofacturacao));
            responses.Add(Regra20(token, periodofacturacao));
            responses.Add(Regra11(token, periodofacturacao));
            responses.Add(Regra12(token, periodofacturacao));
            responses.Add(Regra13(token, periodofacturacao));
            responses.Add(Regra15(token, periodofacturacao));
            responses.Add(Regra16(token, periodofacturacao));
            responses.Add(Regra17(token, periodofacturacao));
            responses.Add(Regra23(token, periodofacturacao));

            return responses.ToArray();

        }

        [HttpGet]
        public IResponse[] ExecutePhase3(Guid token, int periodofacturacao)
        {
            List<IResponse> responses = new List<IResponse>();
            ObjectParameter result = new ObjectParameter("Result", typeof(string));
            IResponse response = new BillingResponse();

            responses.Add(CheckForErrorsPhase1(token, periodofacturacao));

            return responses.ToArray();

        }


        [HttpGet]
        public IResponse DataCleanUp(Guid token)
        {
            _context.CleanUpWorkerTables(token);
            IResponse response = new BillingResponse();
            response.Response = "Data Clean Up complete";
            response.Status = 100;
            response.Phase = 8;

            return response;
        }

        [HttpGet]
        public IResponse[] performbilling(Guid token, int periodofacturacao, string clientes)
        {
            List<IResponse> responses = new List<IResponse>();
            ObjectParameter result = new ObjectParameter("Result", typeof(string));
            IResponse response = new BillingResponse();
            string[] r;

            
            responses.Add(DefineCustomers(token, periodofacturacao,clientes));
            responses.Add(FillCustomerTable(token));
            responses.Add(FillContractTable(token, periodofacturacao));
            responses.Add(FillAuxTables(token, periodofacturacao));
            responses.Add(Regra1(token, periodofacturacao));
            responses.Add(Regra2(token, periodofacturacao));
            responses.Add(Regra3(token, periodofacturacao));
            responses.Add(Regra4(token, periodofacturacao));
            responses.Add(Regra5(token, periodofacturacao));
            responses.Add(Regra6(token, periodofacturacao));
            responses.Add(Regra7(token, periodofacturacao));
            responses.Add(Regra8(token, periodofacturacao));
            responses.Add(Regra19(token, periodofacturacao));
            responses.Add(Regra20(token, periodofacturacao));
            responses.Add(Regra11(token, periodofacturacao));
            responses.Add(Regra12(token, periodofacturacao));
            responses.Add(Regra13(token, periodofacturacao));
            responses.Add(Regra15(token, periodofacturacao));
            responses.Add(Regra16(token, periodofacturacao));
            responses.Add(Regra17(token, periodofacturacao));
            responses.Add(Regra23(token, periodofacturacao));
            responses.Add(CheckForErrorsPhase1(token, periodofacturacao));
         
            return responses.ToArray();
        }

        [HttpGet]
        public IResponse Regra1(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA1(periodofacturacao,"",token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra2(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA2(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra3(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA3(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra4(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA4(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra5(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA5(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra6(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA6(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra7(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA7(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra8(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA8(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra9(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA9(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra10(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA10(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra11(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA11(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra12(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA12(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra13(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA13(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra14(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA14(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra15(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA15(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra16(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA16(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra17(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA17(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra18(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA18(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra19(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA19(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra20(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA20(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra21(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA21(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);
            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra22(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA22(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);

            response.Phase = 2;

            return response;
        }
        [HttpGet]
        public IResponse Regra23(Guid token, int periodofacturacao)
        {

            ObjectParameter result = new ObjectParameter("Result", typeof(string));

            int res = _context.MRF_REGRA23(periodofacturacao, "", token, result);

            string[] r = result.Value.ToString().Split('|');
            IResponse response = new BillingResponse();
            response.AssociatedToken = token;
            response.Response = r[1] + "-" + r[2];
            response.Status = int.Parse(r[0]);

            response.Phase = 2;

            return response;
        }

        
    }

}

