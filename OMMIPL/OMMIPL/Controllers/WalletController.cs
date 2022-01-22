using OMMIPL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMMIPL.Controllers
{
    public class WalletController : BaseController
    {
        // GET: User
        public ActionResult Wallet()
        {
            return View();
        }
        public ActionResult E_WalletReport()
        {
            List<User> lst = new List<User>();
            User model = new User();
            model.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds11 = model.GetEwalletDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    User Obj = new User();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["name"].ToString();
                    Obj.RequestId = r["PK_RequestID"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.Image = r["ImageURL"].ToString();
                    Obj.BankName = r["BankName"].ToString();
                    Obj.BankBranch = r["BankBranch"].ToString();
                    Obj.DDChequeNo = r["ChequeDDNo"].ToString();
                    Obj.DDChequeDate = r["ChequeDDDate"].ToString();
                    lst.Add(Obj);
                }
                model.lstReports = lst;
            }
            return View(model);
        }
        public ActionResult TXNLadget()
        {
            User model = new User();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.PK_UserId = Session["PK_UserId"].ToString();
            List<User> Lst = new List<User>();
            DataSet ds11 = model.GetEwalletLedger();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    User Obj = new User();
                    Obj.CreditAmount = r["CreditAmount"].ToString();
                    Obj.Narration = r["Narration"].ToString();
                    Obj.DebitAmount = r["DebitAmount"].ToString();
                    Obj.Date = r["date"].ToString();
                    Lst.Add(Obj);
                }
                model.lstLedget = Lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult TXNLadget(User model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.PK_UserId = Session["PK_UserId"].ToString();
            List<User> Lst = new List<User>();
            int i = 0;
            DataSet ds11 = model.GetEwalletLedger();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (i < 25)
                    {
                        User Obj = new User();
                        Obj.CreditAmount = r["CreditAmount"].ToString();
                        Obj.DebitAmount = r["DebitAmount"].ToString();
                        Obj.Date = r["date"].ToString();
                        Lst.Add(Obj);
                    }
                    i = i++;
                }
                model.lstLedget = Lst;
            }
            return RedirectToAction("TXNLadget", "Wallet");
        }
        public ActionResult EwalletRequest()
        {
            #region ddlPaymentMode
            User obj = new User();
            int count = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet ds1 = obj.GetPaymentModeDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlPaymentMode = ddlPaymentMode;

            #endregion
            return View(obj);
        }
        [HttpPost]
        [ActionName("EwalletRequest")]
        public ActionResult SaveEwalletRequest(User model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../UploadDocument/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Comman.ConvertToSystemDate(model.DDChequeDate, "dd/MM/yyyy");
                model.AddedBy = Session["PK_UserId"].ToString();
                DataSet ds = model.SaveEwalletRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Ewallet"] = "Ewallet Request save successfully";
                    }
                    else
                    {
                        TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ewallet"] = ex.Message;
            }
            return RedirectToAction("EwalletRequest", "Wallet");
        }
        public ActionResult Withdraw()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Withdraw")]
        public ActionResult Withdraw(User obj)
        {
            obj.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds = obj.WithdrawlRequest();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        @TempData["Ewallet"] = "Withdrawl Successfully";
                    }
                    else
                    {
                        @TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                @TempData["Ewallet"] = ex.Message;
            }
            return RedirectToAction("Withdraw", "Wallet");
        }
    }
}