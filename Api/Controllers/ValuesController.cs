namespace Api.Controllers
{
    using System;
	using System.Collections.Generic;
    using System.Globalization;
	using System.Linq;
	using System.Threading;
    using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Localization;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.Options;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    

    [ServiceFilter(typeof(LanguageActionFilter))]
    [Route("api/{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[controller]")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IStringLocalizer<ValuesController> _localizer;

        public ValuesController(IStringLocalizer<ValuesController> localizer, IOptions<RequestLocalizationOptions> localizationOptions)
        {
var test = localizationOptions; 
            _localizer = localizer;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var x = CultureInfo.CurrentCulture;
            var y = CultureInfo.CurrentUICulture;
            //var z = Thread.CurrentThread.CurrentCulture;

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
