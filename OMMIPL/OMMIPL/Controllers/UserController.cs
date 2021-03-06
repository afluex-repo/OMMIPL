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
        public ActionResult UserDashboard()
        {
            User model = new User();
            List<Game> lst = new List<Game>();
            List<Game> lstColor = new List<Game>();
            List<Game> lstPrediction = new List<Game>();
            List<Game> lstResult = new List<Game>();
            model.PK_UserId = Session["PK_UserId"].ToString();
            model.FK_GameId = "7";
            DataSet ds = model.GetMainBalance();
            DataSet ds1 = model.GetAllGames();
            DataSet ds2 = model.GetUserGamePrediction();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
            {
                model.FK_PeriodId = ds2.Tables[1].Rows[0]["PK_PeriodId"].ToString(); 
                model.PeriodNo = ds2.Tables[1].Rows[0]["PeriodNo"].ToString();
                model.StartTime = ds2.Tables[1].Rows[0]["StartTime"].ToString();
                model.EndTime = ds2.Tables[1].Rows[0]["EndTime"].ToString();
                model.time = TimeSpan.Parse(ds2.Tables[1].Rows[0]["Duration"].ToString());
                model.duration = model.time.ToString("mm\\:ss");
            }
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[2].Rows.Count > 0)
            {
                model.FK_PeriodId2 = ds2.Tables[2].Rows[0]["PK_PeriodId"].ToString();
                model.time = TimeSpan.Parse(ds2.Tables[2].Rows[0]["Duration"].ToString());
                model.duration2 = model.time.ToString("mm\\:ss");

            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.MainBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
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
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    Game obj = new Game();
                    obj.PeriodNo = r["PeriodNo"].ToString();
                    obj.ColorName = r["ChosenColor"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"]);
                    lstPrediction.Add(obj);
                }
                model.lstPrediction = lstPrediction;
            }
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    Game obj = new Game();
                    obj.PeriodNo = r["PeriodNo"].ToString();
                    obj.ColorName = r["ChosenColor"].ToString();
                    obj.ResultantColor = r["ResultantColor"].ToString();

                    lstResult.Add(obj);
                }
                model.lstResult = lstResult;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[1].Rows)
                {
                    Game obj = new Game();
                    obj.FK_ColorId = r["PK_ColorId"].ToString();
                    obj.ColorName = r["Color"].ToString();
                    lstColor.Add(obj);
                }
                model.lstColor = lstColor;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
            {
                model.ColorName = ds1.Tables[2].Rows[0]["ColorName"].ToString().ToLower();
            }
            else
            {
                model.ColorName = "red";
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
                obj.PK_UserId = Session["PK_UserId"].ToString();
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

        public ActionResult GameDetailsById(string GameId)
        {
            User model = new User();
            List<Game> lstColor = new List<Game>();
            List<Game> lstPrediction = new List<Game>();
            model.FK_UserId = Session["PK_UserId"].ToString();
            model.FK_GameId = GameId;
            try
            {
                DataSet ds = model.GetGameDetailsById();
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    model.FK_GameId = ds.Tables[0].Rows[0]["PK_GameId"].ToString();
                //    model.GameName = ds.Tables[0].Rows[0]["GameName"].ToString();
                //    model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                //    //model.Document = ds.Tables[0].Rows[0]["Document"].ToString();
                //    model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                //    model.Duration = ds.Tables[0].Rows[0]["GameTime"].ToString();
                //}
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        model.ColorName = ds.Tables[2].Rows[0]["ColorName"].ToString();
                    }
                    else
                    {
                        model.ColorName = "";
                    }
                    model.PeriodNo = ds.Tables[0].Rows[0]["PeriodNo"].ToString();
                    model.StartTime = ds.Tables[0].Rows[0]["StartTime"].ToString();
                    model.EndTime = ds.Tables[0].Rows[0]["EndTime"].ToString();
                    model.FK_PeriodId = ds.Tables[0].Rows[0]["PK_PeriodId"].ToString();
                    model.time = TimeSpan.Parse(ds.Tables[0].Rows[0]["Duration"].ToString());
                    model.duration = model.time.ToString("mm\\:ss");
                }
                if(GameId=="7")
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Game obj = new Game();
                            obj.PeriodNo = r["PeriodNo"].ToString();
                            obj.ColorName = r["ChosenColor"].ToString();
                            obj.ResultantColor = r["ResultantColor"].ToString();
                            obj.Amount = Convert.ToDecimal(r["Amount"]);
                            lstPrediction.Add(obj);
                        }
                        model.lstPrediction = lstPrediction;
                    }
                }
                else if(GameId == "8")
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Game obj = new Game();
                            obj.PeriodNo = r["PeriodNo"].ToString();
                            obj.ColorName = r["Fk_ChosenColorId"].ToString();
                            obj.ResultantColor = r["FK_ResultId"].ToString();
                            obj.Amount = Convert.ToDecimal(r["Amount"]);
                            lstPrediction.Add(obj);
                        }
                        model.lstPrediction = lstPrediction;
                    }
                }
                else
                {

                }
                
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GameStart(string ColorId, string GameId, string Amount, string PeriodId)
        {
            User model = new User();
            model.FK_ColorId = ColorId;
            model.FK_GameId = GameId;
            model.PK_UserId = Session["PK_UserId"].ToString();
            model.Amount = Amount;
            model.FK_PeriodId = PeriodId;
            DataSet ds = model.GameStart();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataSet ds1 = model.GetMainBalance();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    model.BalanceAmount = ds1.Tables[0].Rows[0]["amount"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    model.Message = "1";
                    model.FK_ColorId = ds.Tables[0].Rows[0]["FK_ColorId"].ToString();
                    model.EndTime = ds.Tables[0].Rows[0]["TimeValidity"].ToString();
                    if (ColorId == model.FK_ColorId)
                    {
                        model.Result = "Yes";
                    }
                    else
                    {
                        model.Result = "No";
                    }
                }
                else
                {
                    model.Message = "0";
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAccountDetails()
        {
            User Obj = new User();
            Obj.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds = Obj.UserProfile();
            if(ds!=null && ds.Tables[0].Rows.Count>0 && ds.Tables.Count>0)
            {
                Obj.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                Obj.IFSC = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                Obj.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                Obj.BankBranch = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                Obj.HolderName = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
            }
            return View(Obj);
        }
        [HttpPost]
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

        //public ActionResult TXNLadget()
        //{
        //    List<User> Lst = new List<User>();
        //    User model = new User();
        //    model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
        //    model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
        //    model.PK_UserId = Session["PK_UserId"].ToString();

        //    DataSet ds11 = model.GetEwalletLedger();
        //    if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds11.Tables[0].Rows)
        //        {
        //            User Obj = new User();
        //            Obj.CreditAmount = r["CreditAmount"].ToString();
        //            Obj.DebitAmount = r["DebitAmount"].ToString();
        //            Obj.Narration = r["Narration"].ToString();
        //            Obj.Date = r["date"].ToString();
        //            Lst.Add(Obj);
        //        }
        //        model.lstLedget = Lst;
        //    }
        //    return View(model);
        //}

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword(User model)
        {
            model.PK_UserId = Session["PK_UserId"].ToString();
            if (model.NewPassword == model.CfPassword)
            {
                model.oldPassword = Crypto.Encrypt(model.oldPassword);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
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

        public ActionResult GameReport()
        {
            User model = new User();
            int count = 0;
            List<SelectListItem> ddlGameType = new List<SelectListItem>();
            DataSet dsgame = model.GameTypeDataList();
            if (dsgame != null && dsgame.Tables.Count > 0 && dsgame.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsgame.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlGameType.Add(new SelectListItem { Text = "Select GameType", Value = "0" });
                    }
                    ddlGameType.Add(new SelectListItem { Text = r["GameName"].ToString(), Value = r["PK_GameId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlGameType = ddlGameType;

            List<User> lstGameReport = new List<User>();
            //User model = new User();
            model.LoginId = Session["LoginId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetGameReport();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        User obj = new User();
                        obj.Name = r["FirstName"].ToString();
                        obj.LastName = r["LastName"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.PK_PeriodId = r["PK_PeriodId"].ToString();
                        obj.Fk_ChosenColorId = r["Fk_ChosenColorId"].ToString();
                        obj.FK_ResultId = r["FK_ResultId"].ToString();
                        obj.color = r["color"].ToString();
                        obj.PeriodNo = r["PeriodNo"].ToString();
                        obj.GameName = r["GameName"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.windate = r["windate"].ToString();
                        obj.GameTime = r["Time"].ToString();
                        lstGameReport.Add(obj);
                    }
                    model.lstGameReport = lstGameReport;
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GameReport(User model)
        {
            int count = 0;
            List<SelectListItem> ddlGameType = new List<SelectListItem>();
            DataSet dsgame = model.GameTypeDataList();
            if (dsgame != null && dsgame.Tables.Count > 0 && dsgame.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsgame.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlGameType.Add(new SelectListItem { Text = "Select GameType", Value = "0" });
                    }
                    ddlGameType.Add(new SelectListItem { Text = r["GameName"].ToString(), Value = r["PK_GameId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlGameType = ddlGameType;

            List<User> lstGameReport = new List<User>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetGameReport();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        User obj = new User();
                        obj.Name = r["FirstName"].ToString();
                        obj.LastName = r["LastName"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.PK_PeriodId = r["PK_PeriodId"].ToString();
                        obj.Fk_ChosenColorId = r["Fk_ChosenColorId"].ToString();
                        obj.FK_ResultId = r["FK_ResultId"].ToString();
                        obj.color = r["color"].ToString();
                        obj.PeriodNo = r["PeriodNo"].ToString();
                        obj.GameName = r["GameName"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.windate = r["windate"].ToString();
                        obj.GameTime = r["Time"].ToString();
                        lstGameReport.Add(obj);
                    }
                    model.lstGameReport = lstGameReport;
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult GetGameResponse(string PeriodId,string ColorId)
        {
            User model = new User();
            List<Game> lstColor = new List<Game>();
            model.FK_UserId = Session["PK_UserId"].ToString();
            model.FK_ColorId = ColorId;
            model.FK_PeriodId = PeriodId;
            try
            {
                DataSet ds = model.GetGameResponse();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //model.PeriodNo = ds.Tables[0].Rows[0]["PeriodNo"].ToString();
                    //model.StartTime = ds.Tables[0].Rows[0]["StartTime"].ToString();
                    //model.EndTime = ds.Tables[0].Rows[0]["EndTime"].ToString();
                    model.FK_ResultId = ds.Tables[0].Rows[0]["FK_ResultId"].ToString();
                    model.FK_ColorId = ds.Tables[0].Rows[0]["FK_ColorId"].ToString();
                    model.Message = "1";
                    if (model.FK_ResultId == model.FK_ColorId)
                    {
                        model.Result = "Yes";
                    }
                    else
                    {
                        model.Result = "No";
                    }
                }
                else
                {
                    model.Message = "0";
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}