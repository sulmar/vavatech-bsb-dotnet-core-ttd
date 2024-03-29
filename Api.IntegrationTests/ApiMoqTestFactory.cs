﻿using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Api.IntegrationTests
{
    public class ApiMoqFactory : WebApplicationFactory<Startup>
    {       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {            
            builder.ConfigureTestServices(services =>
            {
                Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);

                mockVehicleRepository
                    .Setup(vr => vr.Get(1))
                    .Returns(new Vehicle());

                mockVehicleRepository
                    .Setup(vr => vr.Get(0))
                    .Returns<Vehicle>(null);

                services.AddSingleton<IVehicleRepository>(mockVehicleRepository.Object);
            });
        }
    }
}
