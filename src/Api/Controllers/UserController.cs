namespace Api.Controllers
{
    using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
	
    using MongoDB.Bson;
    
    using Api.Models;
	using Api.Repositories;

    [Route("[controller]")]
    public class UserController : Controller
    {
	    private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
		//[ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var results = await _repository.Users();

			if (results == null)
				return new StatusCodeResult(204); // 204 No Content
			
			return new ObjectResult(results);
        }

        [HttpGet("{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
        public async Task<IActionResult> Get(string id)
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var results = await _repository.GetById(new ObjectId(id));
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
        }
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe, string returnUrl = "") {

			Console.Write("Login ");
            if (ModelState.IsValid)
            {
                var user = await _repository.Login(username, password) as User;
				
				if(user == null || !user.Active || user.LockedDate >= DateTime.UtcNow){
					return new StatusCodeResult(204); // 204 No Content
				}
				/*
				var hasher = PasswordHasher();
                var result = hasher.VerifyHashedPassword(user, user.Password, password);
				
				//var result = new PasswordH.VerifyHashedPassword(user.PasswordHash, password);
  				if (result == PasswordVerificationResult.Success){
					  var identity = new ClaimsIdentity("Password");
						identity.AddClaim(new Claim("sub", user.UserName));
						identity.AddClaim(new Claim("role", "user"));
						//identity.AddClaim(new Claim("email", user.Email));
						//context.Validated(identity);
                    //await SignInAsync(user, rememberMe);
                    //return RedirectToLocal(returnUrl);
					var temp = new ObjectResult(identity);
					Console.Write("Identity " + username);
					return temp;
				  }*/
				
				/*
				
                if (user != null)
                {
					var identity = new ClaimsIdentity("Password");
						identity.AddClaim(new Claim("sub", user.UserName));
						identity.AddClaim(new Claim("role", "user"));
						//identity.AddClaim(new Claim("email", user.Email));
						//context.Validated(identity);
                    //await SignInAsync(user, rememberMe);
                    //return RedirectToLocal(returnUrl);
					var temp = new ObjectResult(identity);
					Console.Write("Identity " + username);
					return temp;
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }*/
            }
			return NotFound();
            // If we got this far, something failed, redisplay form
            // return View(model);
           // return RedirectToAction("Index", "Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
	        if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
	        return RedirectToAction("Index", "Home");
        }

/*
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            
            ///Open Question- Hear it create claimIdentity. But we nothing add as such Claims but just User object.
            //public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType);

            var identity = await UserManager1.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //var identity = await UserManager1.CreateAsync(user);//, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }*/
        /*
                [HttpPost]
                public IActionResult CreateSpeaker([FromBody] Speaker speaker)
                {            
                    if (!ModelState.IsValid)
                    {
                        //Context.Response.StatusCode = 400;
                       return new StatusCodeResult(400);
                    }

                    _speakerRepository.Add(speaker);

                    //string url = Url.RouteUrl("GetByIdRoute", new { id = speaker.Id.ToString() }, Request.Scheme, Request.Host.ToUriComponent());
                   // Context.Response.StatusCode = 201;
                    //Context.Response.Headers["Location"] = url;

                    //return CreatedAtRoute("GetByIdRoute", new { id = speaker.Id.ToString() }, Request.Scheme, Request.Host.ToUriComponent());       
                    return CreatedAtRoute("GetByIdRoute", new { id = speaker.Id.ToString() },Request.Host.ToUriComponent());
                }

                [HttpDelete("{id:length(24)}")]
                public IActionResult DeleteSpeaker(string id)
                {
                    if (_speakerRepository.Remove(new ObjectId(id)))
                    {
                        return new StatusCodeResult(204); // 204 No Content
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }*/
    }
}