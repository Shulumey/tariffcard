import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from "@angular/core";
import { tap } from "rxjs/operators";
import { BaseNgDestroy, CommissionsService, Complex, ObjectType, SellerType } from "../../../core";
import { BrokerageRegionService } from "../../../core/services/brokerage-region.service";

@Component({
    selector: "app-main-page",
    templateUrl: "./main-page.component.html",
    styleUrls: ["./main-page.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainPageComponent extends BaseNgDestroy implements OnInit {

    public snapshotDateString: string;

    public complexes: Complex[];

    private selectedRegionGroupId: number;
    private selectedSellerType: SellerType = SellerType.developer;
    private selectedObjectType: ObjectType = ObjectType.apartment;

    constructor(
        private _commissionsService: CommissionsService,
        private changeDetectorRef: ChangeDetectorRef,
        private brokerageRegionService: BrokerageRegionService
    ) {
        super();
    }

    public ngOnInit(): void {
        this.subs = [
            this.brokerageRegionService.selectedRegionGroupId$
                .pipe(
                    tap((selectedRegionGroupId: number) => {
                        this.selectedRegionGroupId = selectedRegionGroupId;
                        this.loadData();
                    })
                )
                .subscribe()
        ];
    }

    public onObjectTypeChanged(objectType: ObjectType): void {
        this.selectedObjectType = objectType;
        this.loadData();
    }

    public onSellerTapeChanged(sellerType: SellerType): void {
        this.selectedSellerType = sellerType;
        this.loadData();
    }

    private loadData(): void {
        this._commissionsService.getComplexes(this.selectedRegionGroupId, this.selectedObjectType, this.selectedSellerType)
            .subscribe((x: Complex[]) => {
                this.initialize(x);
                this.changeDetectorRef.detectChanges();
            });
    }

    private initialize(complexes: Complex[]): void {
        this.snapshotDateString = new Date()
            .toLocaleString("ru-ru", {day: "numeric", month: "long", year: "numeric"})
            .replace(" г.", "");

        this.complexes = complexes;
    }
}
