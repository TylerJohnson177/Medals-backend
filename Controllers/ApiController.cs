using Microsoft.AspNetCore.Mvc;
using Nugatory.Models;

namespace CountryApi.Controllers{
    [ApiController, Route("[controller]/country")]
    public class ApiController: ControllerBase{

        private DataContext _dataContext;

        public ApiController(DataContext db){
            _dataContext = db;
        }

        //Get all the countries
        [HttpGet]
        public IEnumerable<Country> Get(){
            return _dataContext.Countries;
        }

        //Get a specific country
        [HttpGet("{id}")]
        public Country Get(int id){
            return _dataContext.Countries.Find(id);
        }

        //Add a new country
        [HttpPost]
        public async Task<ActionResult<Country>> Post([FromBody] Country country){
            _dataContext.Add(country);
            await _dataContext.SaveChangesAsync();
            return country;
        }

        //Delete a country
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
            Country country = await _dataContext.Countries.FindAsync(id);
            
            if(country == null){
                return NotFound();
            }

            _dataContext.Remove(country);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    
    }
}