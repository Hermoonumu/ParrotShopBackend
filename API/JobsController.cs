using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/jobs")]
public class JobsController(IBackgroundJobClient _jobClient,
                            IRecurringJobManager _recurringJob) : ControllerBase
{
}