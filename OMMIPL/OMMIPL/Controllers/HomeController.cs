using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMMIPL.Models;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace OMMIPL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Contact")]
        public ActionResult SaveContact(Home model)
        {
            try
            {
                DataSet ds = model.SaveContact();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Msg"] = "Contact save Successfully";
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
            return RedirectToAction("Contact", "Home");
        }
        public ActionResult Login()
        {
            Home model = new Home();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Home model)
        {
            string FormName = "";
            string ControllerName = "";
            DataSet ds = model.Login();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate")
                {
                    if (model.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                    {
                        Session["PK_UserId"] = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        FormName = "UserDashboard";
                        ControllerName = "User";
                    }
                    else
                    {
                        TempData["Msg"] = "Something went wrong";
                        FormName = "Login";
                        ControllerName = "Home";
                    }
                }
                else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                {
                        Session["PK_AdminId"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                        Session["PK_UserId"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
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
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ForgetPassword")]
        public ActionResult ForgetPassword(Home model)
        {
            try
            {
                DataSet ds = model.GetUserPassword();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                                model.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                                mailbody = "Dear  " + model.Name + ", <br/> Your Password Is : " +model.Password;
                                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                {
                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = true,
                                    Credentials = new NetworkCredential("developer2.afluex@gmail.com", "deve@486")
                                };
                                using (var message = new MailMessage("developer2.afluex@gmail.com", model.Email)
                                {
                                    IsBodyHtml = true,
                                    Subject = "Forget Password",
                                    Body = mailbody
                                })
                                    smtp.Send(message);
                            }
                            catch (Exception ex)
                            {
                                TempData["Msg"] = ex.Message;
                            }
                        }
                        
                        TempData["Msg"] = "Send Password your email-Id successfully";
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
            return RedirectToAction("Login", "Home");
        }
        public ActionResult GenerateGamePeriodForColorX3()
        {
            Home obj = new Home();
            DataSet ds = obj.GenerateGamePeriodForColorX3();
            return View();
        }
        public ActionResult GenerateGameResponseForColorX3()
        {
            Home obj = new Home();
            DataSet ds = obj.GenerateGameResponseForColorX3();
            return View();
        }
        public ActionResult GenerateGamePeriodForJackpotX10()
        {
            Home obj = new Home();
            DataSet ds = obj.GenerateGamePeriodForJackpotX10();
            return View();
        }
        public ActionResult GenerateGameResponseForJackpotX10()
        {
            Home obj = new Home();
            DataSet ds = obj.GenerateGameResponseForJackpotX10();
            return View();
        }

    }
}