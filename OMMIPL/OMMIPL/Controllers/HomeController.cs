using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMMIPL.Models;
using System.Data;

namespace OMMIPL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            Home model = new Home();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Home model)
        {
            string FormName = "";
            string ControllerName = "";
            model.Password = Crypto.Encrypt(model.Password);
            DataSet ds = model.Login();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate")
                {
                    Session["PK_UserId"] = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    Session["DisplayName"] = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    FormName = "UserDashboard";
                    ControllerName = "User";
                }
                else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                {
                    Session["PK_UserId"] = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    Session["DisplayName"] = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    FormName = "AdminDashboard";
                    ControllerName = "Admin";
                }
                else
                {
                    TempData["Msg"] = "Something went wrong";
                    FormName = "Login";
                    ControllerName = "Home";
                }
            }
            else
            {
                TempData["Msg"] = "Invalid Login Id or Password";
                FormName = "Login";
                ControllerName = "Home";
            }
            return RedirectToAction(FormName, ControllerName);
        }
        public ActionResult Registration()
        {
            Home model = new Home();
            return View(model);
        }
        [HttpPost]
        public ActionResult RegistrationAction(Home model)
        {
            try
            {
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Msg"] = "Registered Successfully";
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
            return RedirectToAction("Registration", "Home");
        }
        public ActionResult GetSponsorDetails(string LoginId)
        {
            Home obj = new Home();
            obj.SponsorLoginId = LoginId;
            DataSet ds = obj.GetSponsor();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "1")
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.FK_SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid Sponsor Id"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCity(string Pincode)
        {
            Home model = new Home();
            try
            {
                model.PinCode = Pincode;
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
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