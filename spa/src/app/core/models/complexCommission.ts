import {CommissionType} from "../enums";

export interface ComplexCommission {
    id: number;
    complexId: number;
    complexName: string;
    commissionMin: number | null;
    commissionMax: number | null;
    commissionType: CommissionType | null;
    hasDetail: boolean;
}
