﻿using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WeatherForecastController : BaseApiController
    {
        public WeatherForecastController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpGet]
        public IEnumerable<WeatherForecastDto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
