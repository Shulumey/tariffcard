import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, Output } from "@angular/core";
import { SellerType } from "../../core";

@Component({
    selector: "tc-seller-tabs",
    templateUrl: "./seller-tabs.component.html",
    styleUrls: ["./seller-tabs.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SellerTabsComponent {

    @Input()
    public active: SellerType = SellerType.developer;

    @Input()
    public availableSellerTabs: SellerType[] = [];

    @Output()
    public activeChange = new EventEmitter<SellerType>();

    constructor(private changeDetectorRef: ChangeDetectorRef) {
    }

    public onTabChanged(value: SellerType): void {
        if (this.isActive(value) || this.isDisabled(value)) {
            return;
        }

        this.active = value;
        this.activeChange.emit(value);
        this.changeDetectorRef.detectChanges();
    }

    public isActive(value: SellerType): boolean {
        return value === this.active;
    }

    public isDisabled(value: SellerType): boolean {
        return false;
        //return this.availableSellerTabs.findIndex(tab => value === tab) === -1;
    }
}
