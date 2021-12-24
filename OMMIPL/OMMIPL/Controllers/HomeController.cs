﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMMIPL.Models;
using System.Data;
using System.IO;

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



        public ActionResult UploadQR( string ID)
        {
            Home model = new Home();
            if(ID!=null)
            {
                model.UploadQRId = ID;
                DataSet ds11 = model.GetUploadQRDetails();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    model.MobileNo = ds11.Tables[0].Rows[0]["MobileNo"].ToString();
                    model.Image = "/UploadQR/" + ds11.Tables[0].Rows[0]["UploadFile"].ToString();
                    model.IsActive = Convert.ToBoolean(ds11.Tables[0].Rows[0]["IsActive"]);

                }
            }
          
            return View(model);
        }
        [HttpPost]
        public ActionResult UploadQR(Home model,HttpPostedFileBase postedFile)
        {
            try
            {

                if(model.UploadQRId==null)
                {
                    if (postedFile != null)
                    {
                        model.Image = "../UploadQR/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }

                    model.AddedBy = "1";
                    DataSet ds = model.UploadQR();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["UploadQR"] = "Upload QR Successfully";
                        }
                        else
                        {
                            TempData["UploadQR"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    if (postedFile != null)
                    {
                        model.Image = "../UploadQR/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }

                    model.AddedBy = "1";
                    DataSet ds = model.UpdateUploadQR();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["UploadQR"] = "Record updated Successfully";
                        }
                        else
                        {
                            TempData["UploadQR"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UploadQR"] = ex.Message;
            }
            return RedirectToAction("UploadQR", "Home");
        }

        

        public ActionResult GetUploadQRDetails()
        {
            Home model = new Home();
            List<Home> lst = new List<Home>();
            DataSet ds11 = model.GetUploadQRDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Home Obj = new Home();
                    Obj.UploadQRId = r["PK_UploadQRId"].ToString();
                    Obj.MobileNo = r["MobileNo"].ToString();
                    Obj.Image = r["UploadFile"].ToString();
                    Obj.IsActive = Convert.ToBoolean(r["IsActive"]);
                    lst.Add(Obj);
                }
                model.lstUploadQR = lst;
            }
            return View(model);
        }


        public ActionResult DeleteUploadQR(string ID)
        {
            try
            {
                Home model = new Home();
                model.AddedBy = "1";
                model.UploadQRId = ID;
                DataSet ds = model.DeleteUploadQR();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["UploadQR"] = "Record Deleted Successfully";
                    }
                    else
                    {
                        TempData["UploadQR"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UploadQR"] = ex.Message;
            }
            return RedirectToAction("GetUploadQRDetails", "Home");
        }


        

        public ActionResult Active(string ID)
        {
            try
            {
                Home model = new Home();
                model.AddedBy = "1";
                model.UploadQRId = ID;
                DataSet ds = model.ActiveUploadQR();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["UploadQR"] = "Record Active Successfully";
                    }
                    else
                    {
                        TempData["UploadQR"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UploadQR"] = ex.Message;
            }
            return RedirectToAction("GetUploadQRDetails", "Home");
        }


        public ActionResult InActive(string ID)
        {
            try
            {
                Home model = new Home();
                model.AddedBy = "1";
                model.UploadQRId = ID;
                DataSet ds = model.InActiveUploadQR();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["UploadQR"] = "Record InActive Successfully";
                    }
                    else
                    {
                        TempData["UploadQR"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UploadQR"] = ex.Message;
            }
            return RedirectToAction("GetUploadQRDetails", "Home");
        }

    }
}