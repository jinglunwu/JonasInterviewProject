using AutoMapper;
using JonasDemoApi.Dto;
using JonasDemoApi.EntityFramework;
using JonasDemoApi.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace JonasDemoApi.Controllers.Api
{
    public class MergedUniquesController : ApiController
    {
        public readonly JonasDataContext _context;

        public MergedUniquesController()
        {
            _context = new JonasDataContext();
        }


        //access it using: /api/MergedUniques
        public IHttpActionResult GetMergedUniques()
        {
            try
            {
                return Ok(_context.MergedUniques.ToList().Select(Mapper.Map<MergedUnique, MergedUniqueDto>));
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message); 
            }
        }

        //access it using /api/MergedUniques/100654
        public IHttpActionResult GetMergedUnique(string Id)
        {
            try
            {
                var mergedUnique = _context.MergedUniques.SingleOrDefault(mu => mu.UNITID == Id);

                if (mergedUnique == null)
                    return NotFound();

                return Ok(Mapper.Map<MergedUnique, MergedUniqueDto>(mergedUnique));
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        
        //access it using /api/MergedUniques 
        //Use POST method in postman and copy and paste one entry but change its UNITID to 999999
        [HttpPost]
        public IHttpActionResult CreateMergedUnique(MergedUniqueDto mergedUniqueDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var mergedUnique = Mapper.Map<MergedUniqueDto, MergedUnique>(mergedUniqueDto);

                _context.MergedUniques.Add(mergedUnique);
                _context.SaveChanges();

                return Created(new Uri(Request.RequestUri + "/" + mergedUniqueDto.UNITID), mergedUniqueDto);
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }


        //access it using /api/MergeUniques/999999, if you created a new one with the id 999999
        //Use PUT method in postman and copy and paste this entry but change a few value of its properties to see if they are changed.
        [HttpPut]
        public IHttpActionResult UpdateMergedUnique(string Id, MergedUniqueDto mergedUniqueDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var mergedUniqueInDb = _context.MergedUniques.SingleOrDefault(mu => mu.UNITID == Id);

                if (mergedUniqueInDb == null)
                    return NotFound();

                Mapper.Map(mergedUniqueDto, mergedUniqueInDb);

                _context.SaveChanges();

                return Ok();
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }


        //access it using /api/MergeUniques/999999, if you created a new one with the id 999999
        //Use DELETE method in postman
        [HttpDelete]
        public IHttpActionResult DeleteMergedUnique(string Id)
        {
            try
            {
                var mergedUniqueInDb = _context.MergedUniques.SingleOrDefault(mu => mu.UNITID == Id);

                if (mergedUniqueInDb == null)
                    return NotFound();

                _context.MergedUniques.Remove(mergedUniqueInDb);
                _context.SaveChanges();

                return Ok();
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

    }
}