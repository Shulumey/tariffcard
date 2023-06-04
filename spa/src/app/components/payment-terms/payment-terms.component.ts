import { ChangeDetectionStrategy, Component, Inject} from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
    selector: "payment-terms",
    templateUrl: "payment-terms.component.html",
    styleUrls: ["payment-terms.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class PaymentTermsComponent {
    public conditionsOfPaymentFees: string;

    constructor(private dialogRef: MatDialogRef<PaymentTermsComponent>,
                @Inject(MAT_DIALOG_DATA) public data: {
                    conditionsOfPaymentFees: string;
                }) {
    }
}
