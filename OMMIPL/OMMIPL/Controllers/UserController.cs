using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMMIPL.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OMMIPL.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            User model = new User();
            model.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds = model.GetMainBalance();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                ViewBag.MainBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
           
            return View(model);
        }
        public ActionResult UserProfile()
        {
            Home obj = new Home();
            try
            {
                obj.PK_UserId = Session["PK_UserId"].ToString();
                DataSet ds = obj.UserProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                        obj.FK_SponsorId = ds.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                        obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                        obj.SponsorLoginId = ds.Tables[0].Rows[0]["UserCode"].ToString();
                        obj.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        obj.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                        obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                        obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                        obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult UpdateProfile(Home obj)
        {
            try
            {
                DataSet ds = obj.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Msg"] = "Profile Updated Successfully";
                    }
                    else
                    {
                        TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
            }
            return RedirectToAction("UserProfile", "User");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Game()
        {
            Game model = new Game();
            List<Game> lst = new List<Game>();
            try
            {
                DataSet ds = model.GetAllGames();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Game obj = new Game();
                        obj.FK_GameId = r["PK_GameId"].ToString();
                        obj.GameName = r["GameName"].ToString();
                        obj.Amount = Convert.ToDecimal(r["Amount"]);
                        obj.Duration = r["GameTime"].ToString();
                        obj.Image = r["Image"].ToString();
                        obj.Document = r["Document"].ToString();
                        lst.Add(obj);
                    }
                    model.lst = lst;
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult PlayGame(string Id)
        {
            Game model = new Game();
            List<Game> lstColor = new List<Game>();
            model.FK_GameId = Id;
            try
            {
                DataSet ds = model.GetGameDetailsById();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.FK_GameId = ds.Tables[0].Rows[0]["PK_GameId"].ToString();
                    model.GameName = ds.Tables[0].Rows[0]["GameName"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                    model.Document = ds.Tables[0].Rows[0]["Document"].ToString();
                    model.Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"]);
                    model.Duration = ds.Tables[0].Rows[0]["GameTime"].ToString();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Game obj = new Game();
                        obj.FK_ColorId = r["PK_ColorId"].ToString();
                        obj.ColorName = r["Color"].ToString();
                        lstColor.Add(obj);
                    }
                    model.lst = lstColor;
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
            }
            return View(model);
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
            return RedirectToAction("EwalletRequest", "User");
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
        [HttpPost]
        [ActionName("TXNLadget")]
        public ActionResult TXNLadget(User model)
        {
            
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
                    Obj.DebitAmount = r["DebitAmount"].ToString();
                    Obj.Date = r["date"].ToString();
                    Lst.Add(Obj);
                }
                model.lstLedget = Lst;
            }
            return View(model);
        }

        public ActionResult GameStart(string ColorId,string GameId)
        {
            Game model = new Game();
            model.FK_ColorId = ColorId;
            model.FK_GameId = GameId;
            model.FK_UserId = Session["PK_UserId"].ToString();
            model.GameStartDateTime = DateTime.Now.ToString();
            DataSet ds = model.GameStart();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                if(ds.Tables[0].Rows[0]["Msg"].ToString()=="1")
                {

                }
            }
            return RedirectToAction("GameStart", "User");
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
            return RedirectToAction("Withdraw", "User");
        }

        public ActionResult UpdateAccountDetails()
        {
            return View();
        }
        [HttpPost]
        [ActionName("UpdateAccountDetails")]
        public ActionResult UpdateAccountDetails(User Obj)
        {
            Obj.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds = Obj.UpdateAccountDetails();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        @TempData["Account"] = "AcountNo Added Successfully";
                    }
                    else
                    {
                        @TempData["Account"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                @TempData["Account"] = ex.Message;
            }
            return RedirectToAction("UpdateAccountDetails", "User");
        }

        public ActionResult TXNLadget()
        {
            List<User> Lst = new List<User>();
            User model = new User();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.PK_UserId = Session["PK_UserId"].ToString();
       
            DataSet ds11 = model.GetEwalletLedger();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    User Obj = new User();
                    Obj.CreditAmount = r["CreditAmount"].ToString();
                    Obj.DebitAmount = r["DebitAmount"].ToString();
                    Obj.Date = r["date"].ToString();
                    Lst.Add(Obj);
                }
                model.lstLedget = Lst;
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword( User model)
        {
            model.PK_UserId = Session["PK_UserId"].ToString();
            if (model.NewPassword == model.CfPassword)
            {
                model.oldPassword = Crypto.Encrypt(model.oldPassword);
                model.NewPassword= Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.ChangePsssword();
                 try
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                            @TempData["Msg"] = "Password Change  Successfully";
                            }
                            else
                            {
                            @TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    @TempData["Msg"] = ex.Message;
                    }
               }
            else
            {
                @TempData["Msg"] = "Your Password is Not Match";
            }
            return RedirectToAction("ChangePassword", "User");
        }
    }
}