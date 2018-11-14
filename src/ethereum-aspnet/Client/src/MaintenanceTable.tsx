import React from "react";
import styles from "./styles";

interface MaintenanceTableProps {
    jobs: Job[]
}

export class MaintenanceTable extends React.Component<MaintenanceTableProps> {
    constructor(props: MaintenanceTableProps) {
        super(props);
    }

    private getStatusDescription(status: number) {
        return status == 0 ? "Done" : status == 1 ? "InProgress" : "Cancelled";
    }
    render() {
        return <div className="table-responsive">
            <table className="table">
                <thead className="thead-light">
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Status</th>
                        <th>Sender</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.jobs.map((job, index) => {
                        return <tr key={index}>
                            <td>{job.id}</td>
                            <td style={styles.table.rowName} >{job.name}</td>
                            <td>{job.description}</td>
                            <td style={styles.table.rowStatus}>{this.getStatusDescription(job.status)}</td>
                            <td>{job.creator}</td>
                        </tr>
                    })}
                </tbody>
            </table>
        </div>
    }
}