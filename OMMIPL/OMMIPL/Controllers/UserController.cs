using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMMIPL.Models;
using System.Data;
using System.Data.SqlClient;

namespace OMMIPL.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            return View();
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
                    if(ds.Tables[0].Rows[0]["Msg"].ToString()=="1")
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
            return RedirectToAction("UserProfile","User");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Geame()
        {
            return View();
        }
    }
}