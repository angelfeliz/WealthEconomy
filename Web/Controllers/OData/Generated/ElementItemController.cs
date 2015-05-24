//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Controllers.OData
{
    using BusinessObjects;
    using Facade;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;

    public abstract class BaseElementItemController : BaseODataController
    {
        public BaseElementItemController()
		{
			MainUnitOfWork = new ElementItemUnitOfWork();		
		}

		protected ElementItemUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/ElementItem
        //[Queryable]
        public virtual IQueryable<ElementItem> Get()
        {
			var list = MainUnitOfWork.AllLive;
            return list;
        }

        // GET odata/ElementItem(5)
        //[Queryable]
        public virtual SingleResult<ElementItem> Get([FromODataUri] int key)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(elementItem => elementItem.Id == key));
        }

        // PUT odata/ElementItem(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int key, ElementItem elementItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != elementItem.Id)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(elementItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainUnitOfWork.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok(elementItem);
        }

        // POST odata/ElementItem
        public virtual async Task<IHttpActionResult> Post(ElementItem elementItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(elementItem);
            }
            catch (DbUpdateException)
            {
                if (MainUnitOfWork.Exists(elementItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(elementItem);
        }

        // PATCH odata/ElementItem(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ElementItem> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var elementItem = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (elementItem == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!elementItem.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(elementItem);
            await MainUnitOfWork.UpdateAsync(elementItem);

            return Ok(elementItem);
        }

        // DELETE odata/ElementItem(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var elementItem = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (elementItem == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(elementItem.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class ElementItemController : BaseElementItemController
    {
	}
}
