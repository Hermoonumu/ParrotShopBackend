using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/jobs")]
public class JobsController(IBackgroundJobClient _jobClient,
                            IRecurringJobManager _recurringJob,
                            IRevokedJWTRepository _jwtRevRepo) : ControllerBase
{
}