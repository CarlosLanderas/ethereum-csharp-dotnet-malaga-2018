import React from "react";
import styles from "./styles";
import { MaintenanceService } from "./services/maintenanceService";

interface MaintenanceFormState {
    name: string;
    description: string;
    status: string;
    lastTransaction: string;
}

interface MaintenanceFormProps {
    onTransactionCreated: (tx: string) => void;
}

export class MaintenanceForm extends React.Component<MaintenanceFormProps, MaintenanceFormState> {

    private maintenanceService: MaintenanceService;

    constructor(props: MaintenanceFormProps) {
        super(props);
        this.state = {
            name: '',
            description: '',
            status: "0",
            lastTransaction: ''
        };

        this.maintenanceService = new MaintenanceService();
        this.onChange = this.onChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    private onChange(event: any) {

        this.setState({
            [event.target.name]: event.target.value
        } as any);
    }

    private async submit(e: any) {

        e.preventDefault();

        let transactionResult: any = await this.maintenanceService.SendJob({
            name: this.state.name,
            description: this.state.description,
            status: parseInt(this.state.status)
        });

        this.props.onTransactionCreated(transactionResult.tx);

        this.setState({
            name: '',
            description: '',
            status: '0',
            lastTransaction: transactionResult.tx
        });
    }

    render() {
        return <div id="form">
            <h6 style={styles.header}>Publish maintenance to Blockchain</h6>
            <form>
                <div className="form-group">
                    <label>Name</label>
                    <input name="name" type="text" onChange={this.onChange} value={this.state.name} className="form-control" id="nameinput" />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <input name="description" type="text" onChange={this.onChange} value={this.state.description} className="form-control" id="descriptioninput" />
                </div>

                <label>Status</label>
                <div className="form-group">
                    <select name="status" value={this.state.status} onChange={this.onChange}>
                        <option value="0">Done</option>
                        <option value="1">InProgress</option>
                        <option value="2">Cancelled</option>
                    </select>
                </div>

                <button onClick={this.submit} className="btn btn-primary">Enviar</button>
                <p style={{wordBreak: 'break-word', marginTop: '10px'}}>
                    <span style={styles.tx.span}>Last transaction:</span>
                    <span> {this.state.lastTransaction}</span>
                </p>
            </form>
        </div>
    }
}