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
            return View();
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



        public ActionResult Approve(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.RequestId = Id;
                DataSet ds = model.Approv();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Game"] = "Record Approved Successfully";
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
            return RedirectToAction("reports", "Admin");
        }

        public ActionResult Decline(string Id)
        {
            try
            {
                Admin model = new Admin();
                model.AddedBy = "1";
                model.RequestId = Id;
                DataSet ds = model.Decline();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Game"] = "Record Declined Successfully";
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
            return RedirectToAction("reports", "Admin");
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
            return RedirectToAction("reports", "Admin");
        }


        public ActionResult QRMaster()
        {
            return View();
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


    }
}