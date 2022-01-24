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
    public class AdminController : AdminBaseController
    {
        // GET: User
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ContactList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetContactDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.Name = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.Address = r["Address"].ToString();
                    Obj.Message = r["Massage"].ToString();
                    lst.Add(Obj);
                }
                model.lstContact = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ContactList")]
        public ActionResult ContactList(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetContactDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.Name = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.Address = r["Address"].ToString();
                    Obj.Message = r["Massage"].ToString();
                    lst.Add(Obj);
                }
                model.lstContact = lst;
            }
            return View(model);
        }


        public ActionResult GameMaster(string Id)
        {
            Admin model = new Admin();
            if (Id != null)
            {
                model.GameId = Id;
                DataSet ds11 = model.GetGameDetails();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds11.Tables[0].Rows[0]["GameName"].ToString();
                    model.Amount = ds11.Tables[0].Rows[0]["Amount"].ToString();
                    model.Time = ds11.Tables[0].Rows[0]["GameTime"].ToString();
                    model.Image1 = "/UploadImage/" + ds11.Tables[0].Rows[0]["Image"].ToString();
                    model.Image2 = "/UploadRule/" + ds11.Tables[0].Rows[0]["Document"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("GameMaster")]
        public ActionResult SaveGameMaster(Admin model, HttpPostedFileBase postedFile1, HttpPostedFileBase postedFile)
        {
            try
            {
                if (model.GameId == null)
                {
                    if (postedFile1 != null)
                    {
                        model.Image1 = "../UploadImage/" + Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                        postedFile1.SaveAs(Path.Combine(Server.MapPath(model.Image1)));
                    }
                    if (postedFile != null)
                    {
                        model.Image2 = "../UploadRule/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image2)));
                    }
                    model.AddedBy = Session["PK_UserId"].ToString();
                    DataSet ds = model.SaveGame();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Game"] = "Record save successfully";
                        }
                        else
                        {
                            TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    if (postedFile1 != null)
                    {
                        model.Image1 = "../UploadImage/" + Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                        postedFile1.SaveAs(Path.Combine(Server.MapPath(model.Image1)));
                    }
                    if (postedFile != null)
                    {
                        model.Image2 = "../UploadRule/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image2)));
                    }
                    model.AddedBy = Session["PK_UserId"].ToString();
                    DataSet ds = model.UpdateGame();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Game"] = "Record update successfully";
                        }
                        else
                        {
                            TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("GameMaster", "Admin");
        }

        public ActionResult GameList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("GameList")]
        public ActionResult GameList(Admin model)
        {
            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetGameDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.GameId = r["PK_GameId"].ToString();
                    Obj.Name = r["GameName"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Time = r["GameTime"].ToString();
                    Obj.Image1 = r["Image"].ToString();
                    Obj.Image2 = r["Document"].ToString();
                    lst.Add(Obj);
                }
                model.lstGame = lst;
            }
            return View(model);
        }


        public ActionResult DeleteGameMaster(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.GameId = Id;
                DataSet ds = model.DeleteGame();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Game"] = "Record Deleted Successfully";
                    }
                    else
                    {
                        TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("GameList", "Admin");
        }

        public ActionResult E_WalletReport()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            model.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds11 = model.GetEwalletDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.LoginID = r["LoginId"].ToString();
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


        //public ActionResult Approve(string Id)
        //{
        //    try
        //    {
        //        Admin model = new Admin();
        //        model.AddedBy = "1";
        //        model.RequestId = Id;
        //        DataSet ds = model.Approv();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                TempData["Game"] = "Record Approved Successfully";
        //            }
        //            else
        //            {
        //                TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Game"] = ex.Message;
        //    }
        //    return RedirectToAction("reports", "Admin");
        //}

        public ActionResult EWalletRequestDeclineApprove(string Id, string status)
        {
            try
            {
                Admin model = new Admin();
                model.Status = status;
                model.AddedBy = Session["PK_AdminId"].ToString();
                model.RequestId = Id;
                DataSet ds = model.Decline();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if (status == "Approved")
                        {
                            TempData["Game"] = "Record Approve Successfully";
                        }
                        else
                        {
                            TempData["Game"] = "Record Declined Successfully";
                        }
                    }
                    else
                    {
                        TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("E_WalletReport", "Admin");
        }


        public ActionResult Delete(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.RequestId = Id;
                DataSet ds = model.Delete();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Game"] = "Record Deleted Successfully";
                    }
                    else
                    {
                        TempData["Game"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Game"] = ex.Message;
            }
            return RedirectToAction("E_WalletReport", "Admin");
        }

        public ActionResult DeleteQR(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.PK_QRId = Id;
                DataSet ds = model.DeleteQR();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Msg"] = "Record Deleted Successfully";
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
            return RedirectToAction("QRMaster", "Admin");
        }
        public ActionResult QRMaster()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            DataSet ds = model.QRMasterList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.AccountName = r["AccountName"].ToString();
                    Obj.Number = r["Number"].ToString();
                    Obj.PK_QRId = r["PK_QRId"].ToString();
                    Obj.CreatedDate = r["CreatedDate"].ToString();
                    Obj.UpLoadQR = r["UpLoadQR"].ToString();
                    lst.Add(Obj);
                }
                model.lstReports = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("QRMaster")]
        public ActionResult QRMaster(Admin model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../UploadQRImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.AddedBy = "1";
                DataSet ds = model.SaveQRMaster();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["QRMaster"] = "Record Save Successfully";
                    }
                    else
                    {
                        TempData["QRMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["QRMaster"] = ex.Message;
            }
            return RedirectToAction("QRMaster", "Admin");
        }
         public ActionResult UserRegistrationList()
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
           // model.PK_UserId = Session["PK_UserId"].ToString();
            DataSet ds11 = model.GetRegistrationDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.LoginID = r["LoginID"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.FathersName = r["FathersName"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.AccountNO = r["AccountNO"].ToString();
                    Obj.IFSCCode = r["IFSCCode"].ToString();
                    Obj.Address = r["Address"].ToString();
                    Obj.CreatedDate = r["CreatedDate"].ToString();
                   
                    lst.Add(Obj);
                }
                model.lstRegistration = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UserRegistrationList")]
        public ActionResult UserRegistrationList(Admin model)
        {

            List<Admin> lst = new List<Admin>();
            DataSet ds11 = model.GetRegistrationDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.LoginID = r["LoginID"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.FathersName = r["FathersName"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.AccountNO = r["AccountNO"].ToString();
                    Obj.IFSCCode = r["IFSCCode"].ToString();
                    Obj.Address = r["Address"].ToString();
                    Obj.CreatedDate = r["CreatedDate"].ToString();
                    lst.Add(Obj);
                }
                model.lstRegistration = lst;
            }
            return View(model);
        }
       
        public ActionResult EWalletUserLedger(String LoginID)
        {
            List<Admin> lst = new List<Admin>();
            Admin model = new Admin();
            model.LoginID = LoginID;
            DataSet ds11 = model.GetRegistrationDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                    model.LoginID = ds11.Tables[0].Rows[0]["LoginID"].ToString();
                    model.Name = ds11.Tables[0].Rows[0]["Name"].ToString();
                    model.FathersName = ds11.Tables[0].Rows[0]["FathersName"].ToString();
                    model.MobileNo = ds11.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = ds11.Tables[0].Rows[0]["Email"].ToString();
                    model.AccountNO = ds11.Tables[0].Rows[0]["AccountNO"].ToString();
                    model.IFSCCode = ds11.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.Address = ds11.Tables[0].Rows[0]["Address"].ToString();
                    model.CreatedDate = ds11.Tables[0].Rows[0]["CreatedDate"].ToString();
            }
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[1].Rows)
                {
                    Admin Obj = new Admin();
                    Obj.CrAmount = r["CrAmount"].ToString();
                    Obj.DrAmount = r["DrAmount"].ToString();
                    Obj.Narration = r["Narration"].ToString();
                    Obj.CurrentDate = r["CurrentDate"].ToString();
                    Obj.paymodename = r["paymodename"].ToString();
                    Obj.paymodeid = r["paymodeid"].ToString();
                    Obj.PK_UserId = r["FK_UserId"].ToString();
                 

                    lst.Add(Obj);
                }
                model.lstUserledger = lst;
            }
            return View(model);
        }

        public ActionResult GameReport()
        {
            List<Admin> lstGameReport = new List<Admin>();
            Admin model = new Admin();
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
            model.LoginID = string.IsNullOrEmpty(model.LoginID) ? null : model.LoginID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetGameReport();
            try
            {
                if(ds!=null && ds.Tables[0].Rows.Count>0 && ds.Tables.Count>0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.Name = r["FirstName"].ToString();
                        obj.LastName = r["LastName"].ToString();
                        obj.LoginID = r["LoginId"].ToString();
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
        public ActionResult GameReport(Admin model)
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
            List<Admin> lstGameReport = new List<Admin>();
          
            model.LoginID = string.IsNullOrEmpty(model.LoginID) ? null : model.LoginID;
         //   model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
          //  model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetGameReport();
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.Name = r["FirstName"].ToString();
                        obj.LastName = r["LastName"].ToString();
                        obj.LoginID = r["LoginId"].ToString();
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
    }
}