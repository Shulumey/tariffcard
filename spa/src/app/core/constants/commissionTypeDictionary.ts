import {CommissionType} from "../enums";

export const COMMISSION_TYPE_DICT: { [id in CommissionType]: string} = {
    [CommissionType.percent]: "%",
    [CommissionType.absolute]: "₽",
    [CommissionType.absolutePerSqMeter]: "₽/м²"
};
