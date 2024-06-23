using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Application.DTOs;
using StockApp.Application.Services;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
namespace StockApp.API.Controllers
{
    public class RecommendationController : ControllerBase
    {

        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendations(string userId)
        {
            var recommendations = await _recommendationService.GetRecommendationsAsync(userId);
            return Ok(recommendations);
        }

    }
}




