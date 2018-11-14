
export class MaintenanceService {

    async getJobs(): Promise<Job[]> {
        const response = await fetch("/api/maintenance/jobs");
        return await response.json();
    }

    async SendJob(job: Job): Promise<string> {
        const result = await fetch('api/maintenance/job',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(job)
            })

        return await result.json();
    }

    async getContractAddress(): Promise<Contract> {
        const response = await fetch('api/maintenance/contract-address');
        return await response.json();
    }
}