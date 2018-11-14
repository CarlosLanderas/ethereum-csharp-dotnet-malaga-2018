
interface Job {
    id?: number;
    name: string;
    description: string;
    status: number
    sender?: string;
    creator?: string;
}

interface Contract {
    address: string;
}