using JobTracker.Models;
using JobTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController: ControllerBase
{
    private readonly JobTrackerService _jobTrackerService;

    private static readonly string[] Status = new[]
    {
        "pending", "tech_test", "phone_interview", "in_person_interview", "rejected", "accepted"
    };

    public JobsController(JobTrackerService jobTrackerService)
    {
        _jobTrackerService = jobTrackerService;
    }

    [HttpGet]
    public async Task<List<JobEntry>> GetAll() => await _jobTrackerService.GetAllEntries();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<JobEntry>> GetById(string id)
    {
        var job = await _jobTrackerService.GetEntryById(id);

        if (job == null)
        {
            return NotFound();
        }
        return job;
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<JobEntry>>> GetByLocation(string location)
    {
        var jobList = await _jobTrackerService.GetEntryByLocation(location);
        if (jobList.Count == 0)
        {
            return NotFound();
        }
        return jobList;
    }

    [HttpPost]
    public async Task<IActionResult> Post(JobEntry newJob)
    {

        if (newJob.DateApplied is null)
        {
            newJob.DateApplied = DateTime.UtcNow;
        }

        await _jobTrackerService.CreateEntry(newJob);
        return CreatedAtAction(nameof(GetById), new {id = newJob.Id }, newJob);
    }

    [HttpPut]
    public async Task<IActionResult> Put(string id, JobEntry updatedJob)
    {
        var job = await _jobTrackerService.GetEntryById(id);

        if (job is null)
        {
            return NotFound();
        }
        updatedJob.Id = job.Id;
        await _jobTrackerService.UpdateEntry(id, updatedJob);

        return NoContent();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var job = await _jobTrackerService.GetEntryById(id);

        if (job is null)
        {
            return NotFound();
        }
        await _jobTrackerService.RemoveEntry(id);
        return NoContent();
    }

}
