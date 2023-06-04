import { CommissionType } from "../enums";

export interface ObjectGroup {
    id: number;
    rooms?: number;
    apartmentId?: number;
    apartmentDescription: string;
    commissionType?: CommissionType;
    commissionValue?: number;
    crossRegionAdvancedBookingCoefficient: number;
    isOverriding: boolean;
}
