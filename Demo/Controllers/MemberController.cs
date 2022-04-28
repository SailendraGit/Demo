using Demo.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class MemberController : Controller
    {
        SailendraDBEntities obj =new SailendraDBEntities();
        // GET: Member
        public ActionResult Index(Member mem)
        {
            if (mem != null)
                return View(mem);
            else
            return View();
        }
        [HttpPost]
        public ActionResult AddMember(Member member)
        {
            Member mobj = new Member();
            if (ModelState.IsValid)
            {
                mobj.MemberId = member.MemberId;
                mobj.FullName = member.FullName;
                mobj.Email = member.Email;
                mobj.Mobile = member.Mobile;
            }
            if(member.MemberId ==0)
            {
                obj.Members.Add(mobj);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(mobj).State = EntityState.Modified;
                obj.SaveChanges();
            }
            ModelState.Clear();
            return View("Index");
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var res = obj.Members.ToList();
            return View(res);
        }
        public ActionResult Delete(Member mem)
        {
              var res = obj.Members.Where(x => x.MemberId == mem.MemberId).First();
                obj.Members.Remove(res);
                obj.SaveChanges();
                
            var list = obj.Members.ToList();
            return View("GetAll",list);
        }
    }
}