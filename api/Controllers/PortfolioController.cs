using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Extensions;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, 
        IPortfolioRepository portfolioRepository, IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepository = portfolioRepository;
            _fmpService = fmpService;   
        }   

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio(){
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortofio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortofio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol){
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("Stock does not exist!");
                }
                else
                {
                    await _stockRepo.CreateStockAsync(stock);
                }
            }

            if(stock == null) return BadRequest("Stock not found!");

            var userPortofio = await _portfolioRepository.GetUserPortfolio(appUser);
            if(userPortofio.Any(e=>e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Cannot add same stock to the portfolio.");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepository.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }

            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where( s=> s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filteredStock.Count == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in your portfolio.");
            }

            return Ok();
        }
    }
}