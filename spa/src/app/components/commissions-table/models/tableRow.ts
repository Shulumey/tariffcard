import { CommissionType } from "../../../core";

export class TableRow {
    public id: number;
    public index?: number;
    public groupIndex: number;
    public groupName?: string;
    public name: string;
    public minCommissionValue?: number;
    public maxCommissionValue?: number;
    public commissionType?: CommissionType;
    public crossRegionAdvancedBookingCoefficient: number;
    public urlLandingPrepaymentBooking: string;
    public type: "complex" | "house" | "object" | "overriding-master";
    public expandable: boolean;
    public expanded: boolean = false;
    public first: boolean;
    public last: boolean;
    public rowspan: number;
    public childRows: TableRow[];
    public properties: {[key: string]: any};
}
