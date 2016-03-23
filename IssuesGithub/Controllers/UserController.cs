using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IssuesGithub.Models;


namespace IssuesGithub.Controllers
{
 
    public class UserController : Controller
    {

        private ApplicationDbContext context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

 
        public ActionResult Index()
        {
            var Users = context.Users.Select(user => new UserViewModel {
                Username = user.UserName,
                Id = user.Id,
                Email = user.Email,
                Role = context.Roles.Where(s => s.Id == user.Roles.Select(r => r.RoleId).FirstOrDefault()).FirstOrDefault().Name
            }).ToList();
            var model = new GroupedUserViewModel { Users = Users };
            return View(model);
        }
        public UserController()
        {
            context = new ApplicationDbContext();
        }
        // GET: User
        public ActionResult createUser()
        {
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpGet]
        
        public ActionResult Update(String id) {
            var userFromUserMaganer = UserManager.FindById(id);
            var role = userFromUserMaganer.Roles.Select(r => r.RoleId).FirstOrDefault();
            var roleName = context.Roles.Where(s => s.Id == role).Select(s => s.Name).ToList().First();
            var user = new UserViewModel {
                Username= userFromUserMaganer.UserName,
                Role = roleName,
                Email = userFromUserMaganer.Email,
                Id = userFromUserMaganer.Id
        };
            
            ViewBag.Role= new SelectList(context.Roles.ToList(),"Name","Name",roleName);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserViewModel model)
        {

            if (ModelState.IsValid)
            {

                var user = UserManager.FindById(model.Id);
                user.UserName = model.Username;
                user.Email = model.Email;

                var userOldRolesId = user.Roles.Select(x => x.RoleId).ToArray();
                var userOldRolesNames = context.Roles.Where(x => userOldRolesId.Contains(x.Id)).Select(x => x.Name).ToArray();
                await UserManager.RemoveFromRolesAsync(user.Id,userOldRolesNames);

                //var newRoleName = context.Roles.Where(x=> x.Id ==)
                UserManager.AddToRole(user.Id,model.Role);

                var result= await UserManager.UpdateAsync(user);
                
                if (result.Succeeded)
                {
                    await context.SaveChangesAsync();
                    return Redirect("/User");
                    
                }
                AddErrors(result);
            }
            return RedirectToAction("Update", "User",new {
                id=model.Id
            });
            


        }
        [HttpPost]
        public ActionResult Delete(String Id)
        {
            var user = UserManager.FindById(Id);
            var roles = user.Roles;
            var logins = user.Logins;

            if (logins.Count() > 0) {
                foreach (var item in logins.ToList())
                {
                    UserManager.RemoveLogin(Id,new UserLoginInfo (
                        item.LoginProvider,
                        item.ProviderKey
                    ));
                }
            }
            

            if (roles.Count() > 0)
            {
                foreach (var item in roles.ToList())
                {
                    // item should be the name of the role
                    var result = UserManager.RemoveFromRole(user.Id, item.RoleId);
                }
            }
            UserManager.Delete(user);
            return Redirect("/User");
        }

        [HttpPost]
   
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> createUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await this.UserManager.AddToRoleAsync(user.Id, model.Name);
                   

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return Redirect("/User");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}