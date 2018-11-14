import React, { Fragment } from "react";
import { Header } from "./Header";
import { MaintenanceTable } from "./MaintenanceTable";
import { MaintenanceService } from "./services/maintenanceService";
import { MaintenanceForm } from "./MaintenanceForm";

import {
    ToastContainer,
    ToastMessageAnimated
} from "react-toastr"

interface MainState {
    jobs: Job[];
    contractAddress: string;
}

export class Main extends React.Component<any, MainState> {

    private maintenanceService: MaintenanceService;
    private container: any;

    constructor(props: any) {
        super(props);

        this.state = {
            jobs: [],
            contractAddress: ''
        };

        this.maintenanceService = new MaintenanceService();
        this.onTransactionCreated = this.onTransactionCreated.bind(this);
    }
    async componentDidMount() {

        let contractAddressPromise = this.maintenanceService.getContractAddress();        
        this.initPolling();

        this.setState({
            contractAddress: (await contractAddressPromise).address
        });
    }

    async initPolling() {
        setInterval(async () => {
            const jobs = await this.maintenanceService.getJobs();
            this.setState({
                jobs
            });
        }, 500);
    }

    onTransactionCreated(transactionHash: string) {
        this.container.success(`Transaction created: ${transactionHash}`);
    }

    render() {
        return (
            <Fragment>
                <Header/>
                <div className="container">
                    <div className="row" style={{marginBottom: "30px"}}>
                        <div className="col-md-12">
                            <span><strong>Contract Address:</strong> {this.state.contractAddress}</span>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-3">
                            <MaintenanceForm onTransactionCreated={this.onTransactionCreated} />
                        </div>
                        <div className="col-md-9">
                            <MaintenanceTable jobs={this.state.jobs} />
                        </div>
                    </div>
                </div>
                <ToastContainer ref={(input: any) => this.container = input}
                    className="toast-top-right"
                    toastMessageFactory={React.createFactory(ToastMessageAnimated)}
                />
            </Fragment>
        )
    }
}