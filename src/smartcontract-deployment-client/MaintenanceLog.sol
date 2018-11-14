pragma solidity ^0.4.24;

contract MaintenanceLog {
    
    enum Status { Done, InProgress, Cancelled }
    
    struct Job {
        uint id;
        string name;
        string description;
        address creator;
        Status status;
    }
    
    Job[] public jobs;
    address private owner;
    
    constructor() public {
        owner = msg.sender;
    }
    
    function storeJob(string name, string description, Status status) public isOwner returns (bool) {
        Job memory job = Job(jobs.length + 1, name, description, msg.sender, status);
        jobs.push(job);
    }
    
    function getJob(uint jobIndex) public view returns (uint, string, string, Status, address) {
        Job memory job = jobs[jobIndex];
        return (job.id, job.name, job.description, job.status, job.creator);
    }
    
    function totalJobs() public view returns (uint) {
        return jobs.length;
    }
    
    modifier isOwner() {
        require(msg.sender == owner);
        _;
    }
}