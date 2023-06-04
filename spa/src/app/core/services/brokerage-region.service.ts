import { Injectable, NgZone } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, map, take } from "rxjs/operators";
import { NmRegionInfoModel } from "nm-widgets";

@Injectable({
    providedIn: "root"
})
export class BrokerageRegionService {
    private selectedRegionGroupId = new BehaviorSubject<number | undefined>(undefined);
    public get selectedRegionGroupId$(): Observable<number> {
        return this.selectedRegionGroupId.pipe(filter((x: number) => x !== undefined), map((x: number) => x as number));
    }

    constructor(
        ngZone: NgZone
    ) {
        window.nmWidgetsLoaded().then(() => {
            window.regionService.regionInfo$
                .pipe(take(1))
                .subscribe((regionsInfo: NmRegionInfoModel) => ngZone.run(() => {
                    this.selectedRegionGroupId.next(regionsInfo.selectedRegionGroupId);
                }));
        });
    }
}
