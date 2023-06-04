import { formatNumber } from "@angular/common";
import { CommissionType } from "../enums";

export class CommissionFormatter {
    public static format(value?: number, type?: CommissionType): string {
        if (!value) {
            return "";
        }
        return formatNumber(value, "ru", type === CommissionType.percent ? "1.2-2" : "1.0");
    }
}
