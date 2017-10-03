using MultiTenancy.Apresentacao.Filters;
using MultiTenancy.Dominio.Business;
using MultiTenancy.Dominio.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace MultiTenancy.Apresentacao.Areas.WebAPI.Controllers
{
    [MultiTenancyWebAPI(true)]
    public class ProdutoController : ApiController
    {
        private ProdutoBusiness produtoBusiness;

        public ProdutoController(ProdutoBusiness produtoBusiness)
        {
            this.produtoBusiness = produtoBusiness;
        }

        // GET api/<controller>
        public IEnumerable<Produto> Get()
        {
            return produtoBusiness.Listar();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}