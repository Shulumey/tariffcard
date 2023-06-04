import { ChangeDetectionStrategy, Component, Input, NgZone } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PaymentTermsComponent } from "../payment-terms/payment-terms.component";

@Component({
    selector: "tc-seller-cell",
    templateUrl: "./seller-cell.component.html",
    styleUrls: ["./seller-cell.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SellerCellComponent {
    @Input()
    public sellerName: string;

    @Input()
    public isSellerCommissionPrepayments: boolean;

    @Input()
    public urlLandingPrepaymentBooking: string;

    @Input()
    public conditionsOfPaymentFees: string;

    constructor(private zone: NgZone,
                private dialog: MatDialog) {
    }

    public openTermsDialog(): void {
        this.zone.run(() => {
            this.dialog.open(PaymentTermsComponent, {
                width: "520px",
                height: "auto",
                data: {
                    conditionsOfPaymentFees: this.conditionsOfPaymentFees ? this.conditionsOfPaymentFees : "Для уточнения информации свяжитесь с менеджером"
                }
            });
        });
    }
}
