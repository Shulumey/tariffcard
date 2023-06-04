import { CommissionType } from "../enums";

export interface Complex {
    id: number;
    sellerId?: number;
    sellerName: string;
    complexName: string;
    commissionType?: CommissionType;
    minCommissionValue?: number;
    maxCommissionValue?: number;
    isAdvancedBooking?: boolean;
    isCommissionSourceFrom100Percent?: boolean;
    isSellerCommissionPrepayments: boolean;
    conditionsOfPaymentFees: string;
    crossRegionAdvancedBookingCoefficient: number;
    urlLandingPrepaymentBooking: string;
    housesCount: number;
}
