using BdCoin.Models;
using BdCoin.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BdCoin.Data;
using AutoMapper;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using BdCoin.ViewModel;

namespace BdCoin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlockchainService _blockchainService;
        private readonly IchainService _chainServices;
        private readonly ItransactionsService _transactionsServices;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(IMapper mapper, IBlockchainService blockchainService, IchainService chainServices, ItransactionsService transactionsServices, ApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _blockchainService = blockchainService;
            _chainServices = chainServices;
            _transactionsServices = transactionsServices;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            Blockchain blockchain = new Blockchain();
            double Balance = 0;
            double Expense = 0;
            double Income = 0;
            int trans = 0;
            int mine = 0;
            var Test = Common_methods();
            var data = Get_chain();
            if (data != null)
            {
                var chain = data.chain;
                blockchain.chain = data.chain;
                foreach (var a in chain)
                {

                    mine = a.transactions.Where(c => c.receiver == "Arafat" && c.amount == 1).Count() + mine;
                    var b = a.transactions.Where(c => c.receiver == "Arafat");
                    var E = a.transactions.Where(c => c.sender == "Arafat");
                    var I = a.transactions.Where(c => c.receiver == "Arafat" && c.amount > 1);
                    foreach (var Inc in I)
                    {
                        Income = Inc.amount + Income;
                    }
                    foreach (var ex in E)
                    {
                        Expense = ex.amount + Expense;
                    }
                    foreach (var c in b)
                    {
                        Balance = c.amount + Balance;
                        trans = a.transactions.Count + trans;
                    }

                }
                blockchain.length = data.length;
                blockchain.number_of_mine = mine;
                blockchain.number_of_transactions = trans;
                blockchain.balance = Balance - Expense + Income;


            }
            if (data == null)
            {
                return NotFound();
            }
            return View(blockchain);
        }

        public int Common_methods()
        {
            int result = -1;
            var data = Get_chain();
            if (data != null)
            {
                result = 1;
                var response = Replace_chain();
                Task.Delay(500).Wait();
                result = 2;
                var response2 = Is_valid();
                Task.Delay(500).Wait();
                result = 3;
                data = Get_chain();
                result = 4;
            }
            return result;
        }

        #region Blockchain_Main_features_View
        public IActionResult Mine_View()
        {
            var data = Mine_block();
            ViewData["Message"] = data.message;
            //ViewData["Index"] = data.Index;
            return View();
        }

        public IActionResult Is_valid_View()
        {
            var data = Is_valid();
            ViewData["Message"] = data.message;

            return View();
        }

        public IActionResult Common_methods_View()
        {
            var data = Common_methods();
            ViewData["Message"] = "Congratulations, you have successfully synced with your remote blockchain!";

            return View();
        }
        #endregion

        #region View
        public IActionResult Blockchain()
        {
            var Test = Common_methods();
            var data = Get_chain();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public IActionResult Chain()
        {
            var Test = Common_methods();
            var d = Get_chain();
            var data = d.chain.ToList();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public IActionResult Transaction()
        {
            var Test = Common_methods();
            var a = Get_chain();
            var data = a.chain.ToList();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        #endregion

        #region Local_Blockchain_store
        public int Local_Blockchain_Insertion()
        {
            int result = -1;
            var data = Get_chain();
            if (data != null)
            {
                Blockchain blockchain = new Blockchain();

                blockchain.length = data.length;
                //blockchain.chain = data.chain;
                _applicationDbContext.blockchains.Add(blockchain);

                //if (_applicationDbContext.SaveChanges() > 0)
                //{
                //    result = 1;
                //    if (data.chain != null)
                //    {
                //        foreach (var obj in data.chain)
                //        {
                //            chain chains = new chain();
                //            chains.blockchainId = blockchain.Id;
                //            chains.index = obj.index;
                //            chains.previous_hash = obj.previous_hash;
                //            chains.proof = obj.proof;
                //            chains.timestamp = obj.timestamp;
                //            //chains.transactions = obj.transactions;
                //            _applicationDbContext.chains.Add(chains);

                //            if (_applicationDbContext.SaveChanges() > 0)
                //            {
                //                result = 2;
                //                if (obj.transactions != null)
                //                {
                //                    foreach (var trans in obj.transactions)
                //                    {
                //                        transactions objtrans = new transactions();
                //                        objtrans.chainId = chains.Id;
                //                        objtrans.sender = trans.sender;
                //                        objtrans.receiver = trans.receiver;
                //                        objtrans.amount = trans.amount;

                //                        _applicationDbContext.transactions.Add(objtrans);
                //                        if (_applicationDbContext.SaveChanges() > 0)
                //                        {
                //                            result = 3;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }

                //}
            }

            return result;
        }
        public int Local_Blockchain_Deletion()
        {
            int result = -1;
            var data = _applicationDbContext.blockchains.ToList();
            if (data != null)
            {
                _applicationDbContext.transactions.RemoveRange();
                if (_applicationDbContext.SaveChanges() > 0)
                {
                    result = 4;
                    _applicationDbContext.chains.RemoveRange();
                    if (_applicationDbContext.SaveChanges() > 0)
                    {
                        result = 5;
                        _applicationDbContext.blockchains.RemoveRange();
                        if (_applicationDbContext.SaveChanges() > 0)
                        {
                            result = 6;
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        #region Blockchain
        public Blockchain Get_chain()
        {
            string UserPort = "5001";
            string DefaultAddress = "http://127.0.0.1:";
            string BaseAddress = DefaultAddress + UserPort + "/";
            string Method = "get_chain";
            string Methodurl = BaseAddress + Method;

            Blockchain model = new Blockchain();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                //HTTP GET
                var responseTask = client.GetAsync(Method);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = String.Format(Methodurl);
                    WebRequest requestObjGet = WebRequest.Create(data);
                    requestObjGet.Method = "GET";
                    HttpWebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                    string datatest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        datatest = sr.ReadToEnd();
                        sr.Close();
                    }
                    if (datatest != null)
                    {
                        dynamic json = JsonConvert.DeserializeObject(datatest);
                        model = JsonConvert.DeserializeObject<Blockchain>(datatest);
                    }

                }
            }
            return model;
        }
        public Blockchain Mine_block()
        {
            string UserPort = "5001";
            string DefaultAddress = "http://127.0.0.1:";
            string BaseAddress = DefaultAddress + UserPort + "/";
            string Method = "mine_block";
            string Methodurl = BaseAddress + Method;

            Blockchain model = new Blockchain();
            //using (var client = new HttpClient())
            //{
            //    //client.BaseAddress = new Uri(BaseAddress);
            //    ////HTTP GET
            //    //var responseTask = client.GetAsync(Method);
            //    //responseTask.Wait();

            //    //var result = responseTask.Result;
            //    //if (result.IsSuccessStatusCode)
            //    //{
                    

            //    //}
                
            //}
            var data = String.Format(Methodurl);
            WebRequest requestObjGet = WebRequest.Create(data);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            string datatest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                datatest = sr.ReadToEnd();
                sr.Close();
            }
            if (datatest != null)
            {
                dynamic json = JsonConvert.DeserializeObject(datatest);
                model = JsonConvert.DeserializeObject<Blockchain>(datatest);
            }
            return model;
        }
        public Blockchain Is_valid()
        {
            string UserPort = "5001";
            string DefaultAddress = "http://127.0.0.1:";
            string BaseAddress = DefaultAddress + UserPort + "/";
            string Method = "is_valid";
            string Methodurl = BaseAddress + Method;

            Blockchain model = new Blockchain();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                //HTTP GET
                var responseTask = client.GetAsync(Method);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = String.Format(Methodurl);
                    WebRequest requestObjGet = WebRequest.Create(data);
                    requestObjGet.Method = "GET";
                    HttpWebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                    string datatest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        datatest = sr.ReadToEnd();
                        sr.Close();
                    }
                    if (datatest != null)
                    {
                        dynamic json = JsonConvert.DeserializeObject(datatest);
                        model = JsonConvert.DeserializeObject<Blockchain>(datatest);
                    }

                }
            }
            return model;
        }
        public Blockchain Replace_chain()
        {
            string UserPort = "5001";
            string DefaultAddress = "http://127.0.0.1:";
            string BaseAddress = DefaultAddress + UserPort + "/";
            string Method = "replace_chain";
            string Methodurl = BaseAddress + Method;

            Blockchain model = new Blockchain();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                //HTTP GET
                var responseTask = client.GetAsync(Method);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = String.Format(Methodurl);
                    WebRequest requestObjGet = WebRequest.Create(data);
                    requestObjGet.Method = "GET";
                    HttpWebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                    string datatest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        datatest = sr.ReadToEnd();
                        sr.Close();
                    }
                    if (datatest != null)
                    {
                        dynamic json = JsonConvert.DeserializeObject(datatest);
                        model = JsonConvert.DeserializeObject<Blockchain>(datatest);
                    }

                }
            }
            return model;
        }

        public ActionResult Add_Transaction(add_transaction add_Transaction)
        {
            using (var client = new HttpClient())
            {
                string UserPort = "5001";
                string DefaultAddress = "http://127.0.0.1:";
                string BaseAddress = DefaultAddress + UserPort + "/";
                string Method = "add_transaction";
                string Methodurl = BaseAddress + Method;
                client.BaseAddress = new Uri(Methodurl);

                //HTTP POST
                var postTask = JsonConvert.SerializeObject(add_Transaction);
                //string jsonString = JsonSerializer.Serialize<add_transaction>(add_Transaction);
                //var postTask = client.PostAsJsonAsync<JsonSerializer>("add_Transaction", JsonSerializer.Create(add_Transaction));
                //var postTask = client.PostAsJsonAsync<add_transaction>("add_Transaction", add_Transaction);
                //postTask.Wait();

                //var result = postTask.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    return RedirectToAction("Index");
                //}
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(add_Transaction);
        }
        //public ActionResult Connect_Node(connect_nodeVM connect_NodeVM)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        string UserPort = "5001";
        //        string DefaultAddress = "http://127.0.0.1:";
        //        string BaseAddress = DefaultAddress + UserPort + "/";
        //        string Method = "connect_node";
        //        string Methodurl = BaseAddress + Method;
        //        client.BaseAddress = new Uri(Methodurl);

        //        //HTTP POST
        //        var postTask = client.PostAsJsonAsync<connect_nodeVM>("connect_NodeVM", connect_NodeVM);
        //        postTask.Wait();

        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        //    return View(connect_NodeVM);
        //}
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
