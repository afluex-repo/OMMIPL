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
    }
}