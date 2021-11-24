using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class CandyRepository : ICandyRepository
    {
        private readonly AppDbContext _appDbContext;

        public CandyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Candy> GetAllCandy 
        { 
           get
            {
                return _appDbContext.Candies.Include(c => c.Category);
            }
        }

        public IEnumerable<Candy> GetCandyOnSale
        {
            get
            {
                return _appDbContext.Candies.Include(c => c.Category).Where(p => p.IsOnSale);
            }
        }

        public IEnumerable<Candy> FrequentyBoughtCandies
        {
            get
            {
                //Logic to implement frequently bought 10 candies
                return _appDbContext.Candies.Include(c => c.Category).Where(p => p.IsOnSale).Take(10);
            }
        }

        public Candy GetCandyById(int candyId)
        {
            return _appDbContext.Candies.FirstOrDefault(c => c.CandyId == candyId);
        }

        public void CreateCandy(Candy candy)
        {

            _appDbContext.Add(candy);
            _appDbContext.SaveChanges();
        }
    }
}
