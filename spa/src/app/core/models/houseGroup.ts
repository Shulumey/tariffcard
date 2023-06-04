import { CommissionType } from "../enums";
import { ObjectGroup } from "./objectGroup";

export interface HouseGroup {
    id: number;
    houseId?: number;
    houseName: string;
    commissionType?: CommissionType;
    commissionValue?: number;
    minMaxCommissionType?: CommissionType;
    minCommissionValue?: number;
    maxCommissionValue?: number;
    crossRegionAdvancedBookingCoefficient: number;
    hasOverriding: boolean;
    objectGroups: ObjectGroup[];
}
